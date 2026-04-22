using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternetClub.RazorPages.Pages.Visits;

public sealed class VisitFormOptions
{
    public SelectList Clients { get; init; } = default!;
    public SelectList Services { get; init; } = default!;
    public SelectList Stuff { get; init; } = default!;
}

