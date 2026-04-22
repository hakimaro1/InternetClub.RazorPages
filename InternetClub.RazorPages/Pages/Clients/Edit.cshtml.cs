using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using InternetClub.RazorPages.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

    [BindProperty]
    [Display(Name = "Дата регистрации")]
    [Required(ErrorMessage = "Введите дату в формате дд.ММ.гггг")]
    [RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "Формат даты: дд.ММ.гггг")]
    public string RegistrationDateText { get; set; } = "";

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
        RegistrationDateText = DateInt.Format(Client.RegistrationDate) ?? "";
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!DateInt.TryParseRu(RegistrationDateText, out var dateInt))
        {
            ModelState.AddModelError(nameof(RegistrationDateText), "Некорректная дата. Пример: 05.04.2026");
        }
        else
        {
            Client.RegistrationDate = dateInt;
        }

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