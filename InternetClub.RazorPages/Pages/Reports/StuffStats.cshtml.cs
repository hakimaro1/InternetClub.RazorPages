using InternetClub.RazorPages.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Reports;

public class StuffStatsModel : PageModel
{
    private readonly InternetClubContext _context;

    public StuffStatsModel(InternetClubContext context)
    {
        _context = context;
    }

    public List<Row> Rows { get; private set; } = new();

    public sealed class Row
    {
        public int StuffId { get; init; }
        public string StuffFullName { get; init; } = "";
        public int VisitsCount { get; init; }
        public decimal TotalRevenue { get; init; }
        public double AverageDurationMinutes { get; init; }
    }

    public async Task OnGetAsync()
    {
        Rows = await _context.Visits.AsNoTracking()
            .Join(
                _context.Stuff.AsNoTracking(),
                v => v.StuffId,
                s => s.Id,
                (v, s) => new { Visit = v, Stuff = s }
            )
            .GroupBy(x => new { x.Stuff.Id, x.Stuff.FullName })
            .Select(g => new Row
            {
                StuffId = g.Key.Id,
                StuffFullName = g.Key.FullName ?? "",
                VisitsCount = g.Count(),
                TotalRevenue = g.Sum(x => x.Visit.TotalPrice),
                AverageDurationMinutes = g.Average(x => (double)x.Visit.Duration)
            })
            .OrderByDescending(x => x.TotalRevenue)
            .ToListAsync();
    }
}

