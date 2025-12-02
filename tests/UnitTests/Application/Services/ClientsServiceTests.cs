using AutoMapper;
using CarMechanicWorkshop.Application.Interfaces;
using CarMechanicWorkshop.Application.Interfaces.Repositories;
using CarMechanicWorkshop.Application.Mappers;
using CarMechanicWorkshop.Application.Services;
using CarMechanicWorkshop.Domain.Entities;
using CarMechanicWorkshop.Shared.DTOs.Clients;
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
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public async Task CreateAsync_ShouldMapAndReturnClientDTO()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<Client>>();
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
        // Arrange
        var mockRepo = new Mock<IRepository<Client>>();
        var mockUow = new Mock<IUnitOfWork>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Client?)null);

        var service = new ClientsService(mockRepo.Object, _mapper, mockUow.Object);

        // Act
        var result = await service.GetByIdAsync(42);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedClient()
    {
        // Arrange
        var existing = new Client { Id = 1, Name = "Old", Address = "A", Email = "old@mail.com" };
        var dto = new UpdateClientDTO { Name = "New" };

        var mockRepo = new Mock<IRepository<Client>>();
        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

        var mockUow = new Mock<IUnitOfWork>();

        var service = new ClientsService(mockRepo.Object, _mapper, mockUow.Object);

        // Act
        var result = await service.UpdateAsync(1, dto);

        // Assert
        Assert.Equal("New", result!.Name);
    }
}
