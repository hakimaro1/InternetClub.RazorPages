using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Visits;

public class CreateModel : PageModel
{
    private readonly InternetClubContext _context;

    public CreateModel(InternetClubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Visit Visit { get; set; } = new Visit
    {
        StartDateTime = new TimeOnly(10, 0),
        Duration = 60
    };

    public VisitFormOptions Options { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        Options = await VisitFormOptionsFactory.BuildAsync(_context);
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

        _context.Visits.Add(Visit);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}

