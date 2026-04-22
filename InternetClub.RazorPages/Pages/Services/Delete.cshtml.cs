using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Services;

public class DeleteModel : PageModel
{
    private readonly InternetClubContext _context;

    public DeleteModel(InternetClubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Service Service { get; set; } = default!;

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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _context.Services.FindAsync(id);
        if (entity is not null)
        {
            _context.Services.Remove(entity);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

