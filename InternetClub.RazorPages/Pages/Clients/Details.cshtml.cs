using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Clients;

public class DetailsModel : PageModel
{
    private readonly InternetClubContext _context;

    public DetailsModel(InternetClubContext context)
    {
        _context = context;
    }

    public Client Client { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _context.Clients
            .AsNoTracking()
            .Include(c => c.Visits)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (entity is null)
        {
            return NotFound();
        }

        Client = entity;
        return Page();
    }
}