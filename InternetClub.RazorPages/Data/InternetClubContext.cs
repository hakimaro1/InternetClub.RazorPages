using InternetClub.RazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetClub.RazorPages.Data;

public class InternetClubContext : DbContext
{
    public InternetClubContext(DbContextOptions<InternetClubContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Stuff> Stuff => Set<Stuff>();
    public DbSet<Visit> Visits => Set<Visit>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .Property(c => c.Balance)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Service>()
            .Property(s => s.PriceMinutes)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Visit>()
            .Property(v => v.TotalPrice)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasOne(v => v.Client)
                .WithMany(c => c.Visits)
                .HasForeignKey(v => v.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(v => v.Service)
                .WithMany(s => s.Visits)
                .HasForeignKey(v => v.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(v => v.Stuff)
                .WithMany(st => st.Visits)
                .HasForeignKey(v => v.StuffId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

