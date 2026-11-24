using HomeLibrary.Models;
using HomeLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeLibrary.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly IBookService _bookService;

        public DetailsModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        public Book Book { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            Book = book;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Partial("DetailModal", this);
            }

            return Page();
        }
    }
}