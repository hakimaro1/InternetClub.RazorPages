namespace InternetClub.RazorPages.Models;

using System.ComponentModel.DataAnnotations;

public class Stuff
{
    public int Id { get; set; }

    [Display(Name = "Фамилия")]
    [Required]
    [StringLength(120, MinimumLength = 3)]
    public string? FullName { get; set; }

    [Display(Name = "Табельный номер")]
    public int Enum { get; set; }

    public List<Visit> Visits { get; set; } = new List<Visit>();
}

