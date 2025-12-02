using CarMechanicWorkshop.Domain.Entities;
using CarMechanicWorkshop.Infrastructure.Persistence;
using CarMechanicWorkshop.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Database.RepositoryTests;

public class CarsRepositoryTests
{
    // private async Task<CarMechanicWorkshopContext> CreateContext()
    // {
    //     var connection = new SqliteConnection("DataSource=:memory:");
    //     await connection.OpenAsync();

    //     var options = new DbContextOptionsBuilder<CarMechanicWorkshopContext>()
    //         .UseSqlite(connection)
    //         .Options;

    //     var context = new CarMechanicWorkshopContext(options);
    //     await context.Database.MigrateAsync(); // important

    //     return context;
    // }

    // [Fact]
    // public async Task AddAsync_ShouldAddCarToDatabase()
    // {
    //     // Arrange
    //     var context = await CreateContext();
    //     var repo = new CarsRepository(context);

    //     var car = new Car
    //     {
    //         PlateNumber = "ABC-123",
    //         Brand = "Volvo",
    //         Model = "V40",
    //         Year = 2003
    //     };

    //     // Act
    //     await repo.AddAsync(car);
    //     await context.SaveChangesAsync();

    //     // Assert
    //     var inDb = await context.Cars.FirstAsync();
    //     Assert.Equal("ABC-123", inDb.PlateNumber);
    // }

    // [Fact]
    // public async Task GetByIdAsync_ShouldReturnCar()
    // {
    //     var context = await CreateContext();
    //     var repo = new CarsRepository(context);

    //     var car = new Car { PlateNumber = "TEST-001", Brand = "BMW", Model = "320i", Year = 2011 };
    //     await repo.AddAsync(car);
    //     await context.SaveChangesAsync();

    //     var result = await repo.GetByIdAsync(car.Id);

    //     Assert.NotNull(result);
    //     Assert.Equal("BMW", result.Brand);
    // }

    // [Fact]
    // public async Task DeleteAsync_ShouldRemoveCar()
    // {
    //     var context = await CreateContext();
    //     var repo = new CarsRepository(context);

    //     var car = new Car { PlateNumber = "XYZ", Brand = "Audi", Model = "A4", Year = 2015 };
    //     await repo.AddAsync(car);
    //     await context.SaveChangesAsync();

    //     await repo.DeleteAsync(car);
    //     await context.SaveChangesAsync();

    //     Assert.Empty(context.Cars);
    // }
}
