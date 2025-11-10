using AutoMapper;
using CarMechanicWorkshop.API.Interfaces;
using CarMechanicWorkshop.API.Mapping;
using CarMechanicWorkshop.API.Services;
using CarMechanicWorkshop.Shared.Models.Database;
using CarMechanicWorkshop.Shared.Models.DTOs;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CarMechanicWorkshop.Tests;

public class ClientsServiceTests
{
    private readonly IMapper _mapper;

    public ClientsServiceTests()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ClientProfile>();
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
    public async Task CreateAsync_ShouldMapAndReturnClientDTO()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<ClientDatabase>>();
        var mockUow = new Mock<IUnitOfWork>();

        var service = new ClientsService(mockRepo.Object, _mapper, mockUow.Object);

        var dto = new CreateClientDTO
        {
            Name = "John Doe",
            Address = "123 Main St",
            Email = "john@example.com"
        };

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        Assert.Equal("John Doe", result.Name);
        Assert.Equal("john@example.com", result.Email);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        var mockRepo = new Mock<IRepository<ClientDatabase>>();
        var mockUow = new Mock<IUnitOfWork>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((ClientDatabase?)null);

        var service = new ClientsService(mockRepo.Object, _mapper, mockUow.Object);

        var result = await service.GetByIdAsync(42);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedClient()
    {
        var existing = new ClientDatabase { Id = 1, Name = "Old", Address = "A", Email = "old@mail.com" };
        var dto = new UpdateClientDTO { Name = "New" };

        var mockRepo = new Mock<IRepository<ClientDatabase>>();
        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

        var mockUow = new Mock<IUnitOfWork>();

        var service = new ClientsService(mockRepo.Object, _mapper, mockUow.Object);

        var result = await service.UpdateAsync(1, dto);

        Assert.Equal("New", result!.Name);
    }
}
