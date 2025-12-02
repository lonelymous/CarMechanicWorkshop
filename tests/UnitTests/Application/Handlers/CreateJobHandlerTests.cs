using AutoMapper;
using CarMechanicWorkshop.Application.Interfaces;
using CarMechanicWorkshop.Infrastructure.Repositories;
using Moq;

namespace UnitTests.Handlers;

public class CreateJobHandlerTests
{
    private readonly Mock<JobsRepository> _repoMock = new();
    private readonly Mock<IUnitOfWork> _uowMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    [Fact]
    public async Task Handle_Should_Create_Job()
    {
    }
}
