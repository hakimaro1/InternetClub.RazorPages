namespace InternetClub.RazorPages.Models;

using System.ComponentModel.DataAnnotations;

public class Client
{
    public int Id { get; set; }

    [Display(Name = "Фамимлия")]
    [StringLength(60, MinimumLength = 2)]
    public string? LastName { get; set; }

    [Display(Name = "Имя")]
    [StringLength(60, MinimumLength = 2)]
    public string? FirstName { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Display(Name = "Дата регистрации")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
    public DateOnly RegistrationDate { get; set; }

    [Range(0, 1_000_000)]
    public decimal Balance { get; set; }

    public List<Visit> Visits { get; set; } = new();
}

