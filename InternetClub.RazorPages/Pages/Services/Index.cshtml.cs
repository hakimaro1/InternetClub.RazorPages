using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Services;

public class IndexModel : PageModel
{
    private readonly InternetClubContext _context;

    public IndexModel(InternetClubContext context)
    {
        _context = context;
    }

    public IList<Service> Services { get; private set; } = new List<Service>();

    public async Task OnGetAsync()
    {
        Services = await _context.Services.AsNoTracking().OrderBy(s => s.Title).ToListAsync();
    }
}

