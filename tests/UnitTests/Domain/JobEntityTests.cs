using CarMechanicWorkshop.Domain.Entities;
using CarMechanicWorkshop.Domain.Enums;

namespace UnitTests.Domain;

public class JobEntityTests
{
    [Fact]
    public void Should_Create_New_Job()
    {
        var job = new Job
        {
            Id = 1,
            ClientId = 1,
            LicensePlate = "SYS-404",
            ManufacturingYear = 2003,
            Category = JobCategoryEnum.Engine,
            Description = "Valami",
            Severity = 5,
            Status = JobStatusEnum.InProgress
        };

        Assert.Equal("SYS-404", job.LicensePlate);
        Assert.Equal(2003, job.ManufacturingYear);
        Assert.Equal("Valami", job.Description);
        Assert.Equal(9.6m, job.EstimatedHours);
    }
}
