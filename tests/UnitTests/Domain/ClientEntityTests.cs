using CarMechanicWorkshop.Domain.Entities;

namespace UnitTests.Domain;

public class ClientEntityTests
{
    [Fact]
    public void Should_Create_New_Client()
    {
        var client = new Client
        {
            Name = "Teszt Elek",
            Address = "Debrecen",
            Email = "tesztelek@test.com"
        };

        Assert.Equal("Teszt Elek", client.Name);
        Assert.Equal("Debrecen", client.Address);
        Assert.Equal("tesztelek@test.com", client.Email);
    }
}
