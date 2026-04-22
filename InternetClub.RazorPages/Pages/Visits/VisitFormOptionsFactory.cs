using InternetClub.RazorPages.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Pages.Visits;

public static class VisitFormOptionsFactory
{
    public static async Task<VisitFormOptions> BuildAsync(InternetClubContext context, int? selectedClientId = null, int? selectedServiceId = null, int? selectedStuffId = null)
    {
        var clients = await context.Clients.AsNoTracking()
            .OrderBy(c => c.LastName)
            .Select(c => new { c.Id, Name = (c.LastName ?? "") + " " + (c.FirstName ?? "") })
            .ToListAsync();

        var services = await context.Services.AsNoTracking()
            .OrderBy(s => s.Title)
            .Select(s => new { s.Id, Name = s.Title })
            .ToListAsync();

        var stuff = await context.Stuff.AsNoTracking()
            .OrderBy(s => s.FullName)
            .Select(s => new { s.Id, Name = s.FullName })
            .ToListAsync();

        return new VisitFormOptions
        {
            Clients = new SelectList(clients, "Id", "Name", selectedClientId),
            Services = new SelectList(services, "Id", "Name", selectedServiceId),
            Stuff = new SelectList(stuff, "Id", "Name", selectedStuffId)
        };
    }
}

