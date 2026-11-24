using HomeLibrary.Data;
using HomeLibrary.Models;
using HomeLibrary.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HomeLibrary.BookServices
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooksAsync(string? searchTerm = null)
        {
            var param = new SqlParameter("@SearchTerm",
                (object?)searchTerm ?? DBNull.Value);

            return await _context.Books
                .FromSqlRaw("EXEC sp_GetBooks @SearchTerm", param)
                .ToListAsync();
        }

        public async Task<int> CreateBookAsync(Book book)
        {
            // Конвертация html в валидный xml
            book.Contents = HtmlToXhtmlConverter.Convert(book.Contents);

            var connection = _context.Database.GetDbConnection();
            await using var command = connection.CreateCommand();
            command.CommandText = "sp_InsertBook";
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@Title", book.Title));
            command.Parameters.Add(new SqlParameter("@Author", book.Author));
            command.Parameters.Add(new SqlParameter("@PublicationYear",
                (object?)book.PublicationYear ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@Contents",
                (object?)book.Contents ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@Genre",
                (object?)book.Genre ?? DBNull.Value));

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            var param = new SqlParameter("@Id", id);

            var books = await _context.Books
                .FromSqlRaw("EXEC sp_GetBookById @Id", param)
                .ToListAsync();

            return books.FirstOrDefault();
        }

        public async Task DeleteBookAsync(int id)
        {
            var param = new SqlParameter("@Id", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteBook @Id", param);
        }

        public async Task UpdateBookAsync(Book book)
        {
            book.Contents = HtmlToXhtmlConverter.Convert(book.Contents);

            var parameters = new[]
            {
                new SqlParameter("@Id", book.Id),
                new SqlParameter("@Title", book.Title),
                new SqlParameter("@Author", book.Author),
                new SqlParameter("@PublicationYear",
                    (object?)book.PublicationYear ?? DBNull.Value),
                new SqlParameter("@Contents",
                    (object?)book.Contents ?? DBNull.Value),
                new SqlParameter("@Genre",
                (object?)book.Genre ?? DBNull.Value)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_UpdateBook @Id, @Title, @Author, @PublicationYear, @Contents, @Genre",
                parameters);
        }
    }
}