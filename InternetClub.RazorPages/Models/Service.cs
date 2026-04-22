namespace InternetClub.RazorPages.Models;

using System.ComponentModel.DataAnnotations;

public class Service
{
    public int Id { get; set; }

    [Display(Name = "Название тарифа")]
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string? Title { get; set; }

    [Display(Name = "Стоимость тарифа")]
    [Range(0.01, 100000)]
    public decimal PriceMinutes { get; set; }

    public List<Visit> Visits { get; set; } = new List<Visit>();
}