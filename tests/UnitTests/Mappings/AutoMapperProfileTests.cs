using AutoMapper;
using CarMechanicWorkshop.Application.Mappers;
using CarMechanicWorkshop.Domain.Entities;
using CarMechanicWorkshop.Shared.DTOs.Clients;
using Microsoft.Extensions.Logging;

namespace UnitTests.Mappings;

public class AutoMapperProfileTests
{
    private readonly IMapper _mapper;

    public AutoMapperProfileTests()
        {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        var config = new MapperConfiguration(cfg =>
        {
                        cfg.AddProfile<ClientProfile>();
            cfg.AddProfile<JobProfile>();
        }, loggerFactory);

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void Should_Map_ClientDto_To_Client()
    {
        var dto = new CreateClientDTO
        {
            Name = "John Doe"
        };

        var entity = _mapper.Map<Client>(dto);

        Assert.Equal("John Doe", entity.Name);
    }
}
