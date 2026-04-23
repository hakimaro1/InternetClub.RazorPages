using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Clients;

public class EditModel : PageModel
{
    private readonly InternetClubContext _context;

    public EditModel(InternetClubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Client Client { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var entity = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        Client = entity;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Client).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Clients.AnyAsync(e => e.Id == Client.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }
}