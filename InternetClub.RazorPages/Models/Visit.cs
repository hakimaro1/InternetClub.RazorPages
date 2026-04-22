namespace InternetClub.RazorPages.Models;

using System.ComponentModel.DataAnnotations;

public class Visit
{
    public int Id { get; set; }

    [Display(Name = "Клиент")]
    public int ClientId { get; set; }

    [Display(Name = "Услуга")]
    public int ServiceId { get; set; }

    [Display(Name = "Сотрудник")]
    public int StuffId { get; set; }

    [Display(Name = "Начало сеанса")]
    public TimeOnly StartDateTime { get; set; }

    [Display(Name = "Продолжительность (минуты)")]
    [Range(1, 24 * 60)]
    public int Duration { get; set; }

    [Display(Name = "Общая сумма")]
    [Range(0, 1_000_000)]
    public decimal TotalPrice { get; set; }

    public Client? Client { get; set; }
    public Service? Service { get; set; }
    public Stuff? Stuff { get; set; }
}

