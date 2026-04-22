using InternetClub.RazorPages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Stuff;

public class DetailsModel : PageModel
{
    private readonly InternetClubContext _context;

    public DetailsModel(InternetClubContext context)
    {
        _context = context;
    }

    public Models.Stuff Stuff { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _context.Stuff.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        Stuff = entity;
        return Page();
    }
}

