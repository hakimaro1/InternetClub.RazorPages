using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Visits;

public class DeleteModel : PageModel
{
    private readonly InternetClubContext _context;

    public DeleteModel(InternetClubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Visit Visit { get; set; } = default!;

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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _context.Visits.FindAsync(id);
        if (entity is not null)
        {
            _context.Visits.Remove(entity);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

