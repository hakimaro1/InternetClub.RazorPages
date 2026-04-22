using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Services;

public class DetailsModel : PageModel
{
    private readonly InternetClubContext _context;

    public DetailsModel(InternetClubContext context)
    {
        _context = context;
    }

    public Service Service { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _context.Services.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        Service = entity;
        return Page();
    }
}

