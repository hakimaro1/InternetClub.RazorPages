using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InternetClub.RazorPages.Pages.Services;

public class CreateModel : PageModel
{
    private readonly InternetClubContext _context;

    public CreateModel(InternetClubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Service Service { get; set; } = new Service();

    public IActionResult OnGet() => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Services.Add(Service);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}

