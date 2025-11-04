using Microsoft.EntityFrameworkCore;
using CarMechanicWorkshop.Shared.Models.Database;

namespace CarMechanicWorkshop.API.Data
{
    public class CarMechanicWorkshopContext : DbContext
    {
        public CarMechanicWorkshopContext(DbContextOptions<CarMechanicWorkshopContext> options) : base(options) { }

        public DbSet<ClientDatabase> Clients => Set<ClientDatabase>();
        public DbSet<JobDatabase> Jobs => Set<JobDatabase>();
    }
}