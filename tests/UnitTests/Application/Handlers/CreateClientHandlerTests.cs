using AutoMapper;
using CarMechanicWorkshop.Application.Interfaces;
using CarMechanicWorkshop.Application.Interfaces.Repositories;
using CarMechanicWorkshop.Domain.Entities;
using CarMechanicWorkshop.Shared.DTOs.Clients;
using Moq;

namespace UnitTests.Handlers;

public class CreateClientHandlerTests
{
    private readonly Mock<IRepository<Client>> _repoMock = new();
    private readonly Mock<IUnitOfWork> _uowMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    [Fact]
    public async Task Handle_Should_Create_Client()
    {
        // Arrange
        var dto = new CreateClientDTO
        {
            Name = "Szabo Armin",
            Address = "Debrecen",
            Email = "szabo.armin.andras@gmail.com"
        };

        // var command = new CreateClientCommand(dto);

        // var expectedClient = new Client
        // {
        //     Name = dto.Name,
        //     Address = dto.Address,
        //     Email = dto.Email
        // };

        // _mapperMock.Setup(m => m.Map<Client>(dto))
        //            .Returns(expectedClient);

        // _uowMock.Setup(u => u.SaveChangesAsync())
        //         .ReturnsAsync(1);

        // var handler = new CreateClientHandler(_repoMock.Object, _uowMock.Object, _mapperMock.Object);

        // // Act
        // await handler.Handle(command, CancellationToken.None);

        // // Assert
        // _repoMock.Verify(r => r.AddAsync(It.IsAny<Client>()), Times.Once);
        // _uowMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}
