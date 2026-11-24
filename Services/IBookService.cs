using HomeLibrary.Models;

namespace HomeLibrary.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetBooksAsync(string? searchTerm = null);
        Task<Book?> GetBookByIdAsync(int id);
        Task<int> CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}