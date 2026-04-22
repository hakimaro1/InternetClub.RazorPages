using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InternetClub.RazorPages.Pages.Stuff;

public class CreateModel : PageModel
{
    private readonly InternetClubContext _context;

    public CreateModel(InternetClubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.Stuff Stuff { get; set; } = new Models.Stuff();

    public IActionResult OnGet() => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Stuff.Add(Stuff);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}

