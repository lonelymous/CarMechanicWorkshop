using CarMechanicWorkshop.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CarMechanicWorkshop.IntegrationTests.Database;

public class MigrationTests
{
    [Fact]
    public async Task Database_ShouldApply_All_Migrations()
    {
        // Use SQLite in-memory for schema validation
        using var connection = new SqliteConnection("DataSource=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        using var context = new AppDbContext(options);

        // Act: Apply all migrations
        var exception = await Record.ExceptionAsync(() => context.Database.MigrateAsync());

        // Assert
        Assert.Null(exception);
    }
}
