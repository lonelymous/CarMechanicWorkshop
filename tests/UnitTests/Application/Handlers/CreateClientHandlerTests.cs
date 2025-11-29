using AutoMapper;
using CarMechanicWorkshop.Application.Clients.Commands;
using CarMechanicWorkshop.Application.Clients.Handlers;
using CarMechanicWorkshop.Application.DTOs.Clients;
using CarMechanicWorkshop.Domain.Entities;
using CarMechanicWorkshop.Application.Interfaces;
using Moq;

namespace UnitTests.Handlers;

public class CreateClientHandlerTests
{
    private readonly Mock<IClientsRepository> _repoMock = new();
    private readonly Mock<IUnitOfWork> _uowMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    [Fact]
    public async Task Handle_Should_Create_Client()
    {
        // Arrange
        var dto = new CreateClientDto
        {
            FirstName = "Armin",
            LastName = "Szabo",
            Phone = "+3620123456"
        };

        var command = new CreateClientCommand(dto);
        var handler = new CreateClientHandler(_repoMock.Object, _uowMock.Object, _mapperMock.Object);

        var expectedClient = new Client("Armin", "Szabo", "+3620123456");

        _mapperMock.Setup(m => m.Map<Client>(dto))
                   .Returns(expectedClient);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        _repoMock.Verify(r => r.AddAsync(It.IsAny<Client>()), Times.Once);
        _uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}
