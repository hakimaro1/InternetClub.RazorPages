using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Visits;

public class DetailsModel : PageModel
{
    private readonly InternetClubContext _context;

    public DetailsModel(InternetClubContext context)
    {
        _context = context;
    }

    public Visit Visit { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _context.Visits
            .AsNoTracking()
            .Include(v => v.Client)
            .Include(v => v.Service)
            .Include(v => v.Stuff)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (entity is null)
        {
            return NotFound();
        }

        Visit = entity;
        return Page();
    }
}

