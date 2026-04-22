using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Clients;

public class IndexModel : PageModel
{
    private readonly InternetClubContext _context;

    public IndexModel(InternetClubContext context)
    {
        _context = context;
    }

    public IList<Client> Clients { get; private set; } = new List<Client>();

    public async Task OnGetAsync()
    {
        Clients = await _context.Clients.AsNoTracking().OrderBy(c => c.LastName).ToListAsync();
    }
}