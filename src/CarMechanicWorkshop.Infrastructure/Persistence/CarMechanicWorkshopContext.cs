using CarMechanicWorkshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarMechanicWorkshop.Infrastructure.Persistence;

public class CarMechanicWorkshopContext : DbContext
{
    public CarMechanicWorkshopContext(DbContextOptions<CarMechanicWorkshopContext> options) : base(options) { }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Job> Jobs => Set<Job>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>().ToTable("Clients");
        modelBuilder.Entity<Job>().ToTable("Jobs");
    }
}