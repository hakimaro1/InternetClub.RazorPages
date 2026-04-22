using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Services;

public class EditModel : PageModel
{
    private readonly InternetClubContext _context;

    public EditModel(InternetClubContext context)
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

        var entity = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        Service = entity;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Service).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Services.AnyAsync(e => e.Id == Service.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }
}

