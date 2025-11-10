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
}