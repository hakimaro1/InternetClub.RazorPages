using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Visits;

public class EditModel : PageModel
{
    private readonly InternetClubContext _context;

    public EditModel(InternetClubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Visit Visit { get; set; } = default!;

    public VisitFormOptions Options { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _context.Visits.FirstOrDefaultAsync(v => v.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        Visit = entity;
        Options = await VisitFormOptionsFactory.BuildAsync(_context, Visit.ClientId, Visit.ServiceId, Visit.StuffId);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Options = await VisitFormOptionsFactory.BuildAsync(_context, Visit.ClientId, Visit.ServiceId, Visit.StuffId);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var servicePrice = await _context.Services.AsNoTracking()
            .Where(s => s.Id == Visit.ServiceId)
            .Select(s => s.PriceMinutes)
            .FirstOrDefaultAsync();

        Visit.TotalPrice = Visit.Duration * servicePrice;

        _context.Attach(Visit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Visits.AnyAsync(e => e.Id == Visit.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }
}

