using InternetClub.RazorPages.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLocalization();
builder.Services
    .AddRazorPages()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();
builder.Services.AddDbContext<InternetClubContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("InternetClubContext")));

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("ru-RU"),
        new CultureInfo("en-US"),
    };

    options.DefaultRequestCulture = new RequestCulture("ru-RU");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();

var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.UseAuthorization();

app.MapPost("/set-culture", async (HttpContext ctx) =>
{
    var form = await ctx.Request.ReadFormAsync();
    var culture = (string?)form["culture"];
    var returnUrl = (string?)form["returnUrl"];

    if (string.IsNullOrWhiteSpace(culture))
    {
        culture = "ru-RU";
    }

    ctx.Response.Cookies.Append(
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        new CookieOptions
        {
            IsEssential = true,
            Expires = DateTimeOffset.UtcNow.AddYears(1),
        });

    static bool IsLocalUrl(string? url) =>
        !string.IsNullOrWhiteSpace(url) &&
        url.StartsWith('/') &&
        !url.StartsWith("//") &&
        !url.StartsWith("/\\");

    if (!IsLocalUrl(returnUrl))
    {
        return Results.Redirect("/");
    }

    return Results.LocalRedirect(returnUrl!);
});

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<InternetClubContext>();
    db.Database.Migrate();
    SeedData.Initialize(db);
}

app.Run();
