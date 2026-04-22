using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Visits;

public class IndexModel : PageModel
{
    private readonly InternetClubContext _context;

    public IndexModel(InternetClubContext context)
    {
        _context = context;
    }

    public IList<Visit> Visits { get; private set; } = new List<Visit>();

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    public async Task OnGetAsync()
    {
        var query = _context.Visits
            .AsNoTracking()
            .Include(v => v.Client)
            .Include(v => v.Service)
            .Include(v => v.Stuff)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(Search))
        {
            var s = Search.Trim();
            query = query.Where(v =>
                (v.Client != null && ((v.Client.LastName ?? "").Contains(s) || (v.Client.FirstName ?? "").Contains(s))) ||
                (v.Service != null && (v.Service.Title ?? "").Contains(s)) ||
                (v.Stuff != null && (v.Stuff.FullName ?? "").Contains(s)));
        }

        Visits = await query.OrderByDescending(v => v.Id).ToListAsync();
    }
}

