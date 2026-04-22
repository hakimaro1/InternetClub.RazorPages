using InternetClub.RazorPages.Data;
using InternetClub.RazorPages.Models;
using InternetClub.RazorPages.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace InternetClub.RazorPages.Pages.Clients;

public class CreateModel : PageModel
{
    private readonly InternetClubContext _context;

    public CreateModel(InternetClubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Client Client { get; set; } = new Client();

    [BindProperty]
    [Display(Name = "Дата регистрации")]
    [Required(ErrorMessage = "Введите дату в формате дд.ММ.гггг")]
    [RegularExpression(@"^\d{2}\.\d{2}\.\d{4}$", ErrorMessage = "Формат даты: дд.ММ.гггг")]
    public string RegistrationDateText { get; set; } = "";

    public IActionResult OnGet()
    {
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

        _context.Clients.Add(Client);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}