using System.Net;
using CarMechanicWorkshop.IntegrationTests.Utilities;
using FluentAssertions;

namespace CarMechanicWorkshop.IntegrationTests.Api;

public class JobsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public JobsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllJobs_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/jobs");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("jobs");
    }
}
