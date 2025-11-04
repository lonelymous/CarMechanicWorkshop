using CarMechanicWorkshop.Shared.Models;
using CarMechanicWorkshop.Shared.Models.Database;

namespace CarMechanicWorkshop.Tests;

public class JobTests
{
    [Fact]
    public void JobtimeEstimation_ShouldBeCalculatedCorrectly()
    {
        var job = new JobDatabase
        {
            Category = JobCategoryEnum.Engine,
            ManufacturingYear = DateTime.Now.Year - 10,
            Severity = 5
        };
        Assert.Equal(8 * 1.5 * 0.6, job.EstimatedHours);
    }
}
