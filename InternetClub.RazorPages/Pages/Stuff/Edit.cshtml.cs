using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Stuff;

public class EditModel : PageModel
{
    private readonly InternetClubContext _context;

    public EditModel(InternetClubContext context)
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

        var entity = await _context.Stuff.FirstOrDefaultAsync(s => s.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        Stuff = entity;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Stuff).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Stuff.AnyAsync(e => e.Id == Stuff.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }
}

