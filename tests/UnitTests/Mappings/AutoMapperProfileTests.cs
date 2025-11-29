using AutoMapper;
using CarMechanicWorkshop.Application.Mappers;

namespace UnitTests.Mappings;

public class AutoMapperProfileTests
{
    private readonly IConfigurationProvider _config;
    private readonly IMapper _mapper;

    public AutoMapperProfileTests()
    {
        _config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ClientProfile>();
            cfg.AddProfile<JobProfile>();
            cfg.AddProfile<CarProfile>();
            // Add all your profiles...
        });

        _mapper = _config.CreateMapper();
    }

    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        _config.AssertConfigurationIsValid();
    }

    [Fact]
    public void Should_Map_ClientDto_To_Client()
    {
        var dto = new CreateClientDto
        {
            FirstName = "John",
            LastName = "Doe",
            Phone = "+3620123456"
        };

        var entity = _mapper.Map<Client>(dto);

        Assert.Equal("John", entity.FirstName);
        Assert.Equal("Doe", entity.LastName);
    }
}
