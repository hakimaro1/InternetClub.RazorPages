using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InternetClub.RazorPages.Pages.Clients;

public class CreateModel : PageModel
{
    private readonly InternetClubContext _context;

    public CreateModel(InternetClubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Client Client { get; set; } = new Client();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Clients.Add(Client);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}