using HomeLibrary.Models;
using HomeLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeLibrary.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly IBookService _bookService;

        public EditModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        [BindProperty]
        public Book Book { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            Book = book;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Partial("EditModal", this);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (!ModelState.IsValid)
            {
                if (isAjax) return Partial("EditModal", this);
                return Page();
            }

            await _bookService.UpdateBookAsync(Book);

            if (isAjax) return new JsonResult(new { success = true });

            return RedirectToPage("Index");
        }
    }
}