using HomeLibrary.Models;
using HomeLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeLibrary.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly IBookService _bookService;

        public IndexModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public List<Book> Books { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            Books = await _bookService.GetBooksAsync(SearchTerm);
        }
    }
}