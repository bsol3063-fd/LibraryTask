using HomeLibrary.Models;
using HomeLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeLibrary.Pages.Books
{
    public class DeleteModel : PageModel
    {
        private readonly IBookService _bookService;

        public DeleteModel(IBookService bookService)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToPage("Index");
        }
    }
}