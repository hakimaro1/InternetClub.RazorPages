using InternetClub.RazorPages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Stuff;

public class DeleteModel : PageModel
{
    private readonly InternetClubContext _context;

    public DeleteModel(InternetClubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Models.Stuff Stuff { get; set; } = default!;

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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _context.Stuff.FindAsync(id);
        if (entity is not null)
        {
            _context.Stuff.Remove(entity);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

