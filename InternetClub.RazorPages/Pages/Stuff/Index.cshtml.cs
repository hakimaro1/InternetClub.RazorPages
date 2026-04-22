using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Stuff;

public class IndexModel : PageModel
{
    private readonly InternetClubContext _context;

    public IndexModel(InternetClubContext context)
    {
        _context = context;
    }

    public IList<Models.Stuff> StuffList { get; private set; } = new List<Models.Stuff>();

    public async Task OnGetAsync()
    {
        StuffList = await _context.Stuff.AsNoTracking().OrderBy(s => s.FullName).ToListAsync();
    }
}

