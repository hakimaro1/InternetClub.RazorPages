using InternetClub.RazorPages.Models;

namespace InternetClub.RazorPages.Data;

public static class SeedData
{
    public static void Initialize(InternetClubContext context)
    {
        if (context.Clients.Any())
        {
            return;
        }

        var clients = new[]
        {
            new Client
            {
                LastName = "Иванов",
                FirstName = "Иван",
                Phone = "+7 900 000-00-01",
                Email = "ivanov@example.com",
                RegistrationDate = new DateOnly(2026, 4, 1),
                Balance = 500.00m
            },
            new Client
            {
                LastName = "Петрова",
                FirstName = "Анна",
                Phone = "+7 900 000-00-02",
                Email = "petrova@example.com",
                RegistrationDate = new DateOnly(2026, 4, 5),
                Balance = 250.00m
            }
        };

        var services = new[]
        {
            new Service { Title = "ПК (поминутно)", PriceMinutes = 2.5m },
            new Service { Title = "Название тарифа", PriceMinutes = 1m }
        };

        var stuff = new[]
        {
            new Stuff { FullName = "Сидоров Сергей", Enum = 1 },
            new Stuff { FullName = "Кузнецова Мария", Enum = 2 }
        };

        context.Clients.AddRange(clients);
        context.Services.AddRange(services);
        context.Stuff.AddRange(stuff);
        context.SaveChanges();

        var visits = new[]
        {
            new Visit
            {
                ClientId = clients[0].Id,
                ServiceId = services[0].Id,
                StuffId = stuff[0].Id,
                StartDateTime = new TimeOnly(10, 0),
                Duration = 60,
                TotalPrice = 150m
            },
            new Visit
            {
                ClientId = clients[1].Id,
                ServiceId = services[0].Id,
                StuffId = stuff[1].Id,
                StartDateTime = new TimeOnly(12, 30),
                Duration = 30,
                TotalPrice = 75m
            }
        };

        context.Visits.AddRange(visits);
        context.SaveChanges();
    }
}