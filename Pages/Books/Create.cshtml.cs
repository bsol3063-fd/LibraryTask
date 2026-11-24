using HomeLibrary.Models;
using HomeLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeLibrary.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly IBookService _bookService;

        public CreateModel(IBookService bookService)
        {
            _bookService = bookService;
        }

        [BindProperty]
        public Book Book { get; set; } = new();

        public IActionResult OnGet()
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return Partial("CreateModal", this);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (!ModelState.IsValid)
            {
                if (isAjax) return Partial("CreateModal", this);
                return Page();
            }

            await _bookService.CreateBookAsync(Book);

            if (isAjax) return new JsonResult(new { success = true });

            return RedirectToPage("Index");
        }
    }
}