using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CarMechanicWorkshop.Tests;

public class JobsServiceTests
{
    private readonly IMapper _mapper;

    public JobsServiceTests()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<JobProfile>();
        }, loggerFactory);

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void AutoMapperConfiguration_IsValid()
    {
        // This ensures mappings are correct and won't throw at runtime
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public async Task CreateAsync_ShouldFail_WhenManufacturingYearIsToLow()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<Job>>();
        var mockUow = new Mock<IUnitOfWork>();

        var service = new JobsService(mockRepo.Object, _mapper, mockUow.Object);

        var dto = new CreateJobDTO
        {
            ClientId = 1,
            LicensePlate = "SYS-404",
            ManufacturingYear = 1750,
            Category = JobCategoryEnum.Engine,
            Description = "Valami",
            Severity = 5,
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(dto));
    }

    [Fact]
    public async Task CreateAsync_ShouldMapAndReturnJobDTO()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<Job>>();
        var mockUow = new Mock<IUnitOfWork>();

        var service = new JobsService(mockRepo.Object, _mapper, mockUow.Object);

        var dto = new CreateJobDTO
        {
            ClientId = 1,
            LicensePlate = "SYS-404",
            ManufacturingYear = 2003,
            Category = JobCategoryEnum.Engine,
            Description = "Valami",
            Severity = 5,
        };

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        Assert.Equal(9.6, Math.Round(result.EstimatedHours, 2));
        Assert.Equal(JobStatusEnum.Accepted, result.Status);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        var mockRepo = new Mock<IRepository<Job>>();
        var mockUow = new Mock<IUnitOfWork>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Job?)null);

        var service = new JobsService(mockRepo.Object, _mapper, mockUow.Object);
        
        // Act
        var result = await service.GetByIdAsync(69);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateStatus_ShouldFail_WhenMovingBackward()
    {
        // Arrange
        var existing = new Job
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
        var dto = new UpdateJobDTO
        {
            Status = JobStatusEnum.Accepted,
        };

        var mockRepo = new Mock<IRepository<Job>>();
        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

        var mockUow = new Mock<IUnitOfWork>();

        var service = new JobsService(mockRepo.Object, _mapper, mockUow.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateAsync(1, dto));
    }

    [Fact]
    public async Task UpdateStatus_ShouldReturnUpdatedJob()
    {
        // Arrange
        var existing = new Job
        {
            Id = 1,
            ClientId = 1,
            LicensePlate = "SYS-404",
            ManufacturingYear = 2003,
            Category = JobCategoryEnum.Engine,
            Description = "Valami",
            Severity = 5,
            Status = JobStatusEnum.Accepted
        };
        var existingEstimatedHours = existing.EstimatedHours;
        var dto = new UpdateJobDTO
        {
            Status = JobStatusEnum.InProgress,
            Severity = 10,
        };

        var mockRepo = new Mock<IRepository<Job>>();
        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

        var mockUow = new Mock<IUnitOfWork>();

        var service = new JobsService(mockRepo.Object, _mapper, mockUow.Object);

        // Act
        var result = await service.UpdateAsync(1, dto);
        
        // Assert
        Assert.Equal(JobStatusEnum.InProgress, result!.Status);
        Assert.NotEqual(existingEstimatedHours, result!.EstimatedHours);
    }
}