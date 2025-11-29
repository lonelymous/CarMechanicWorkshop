using CarMechanicWorkshop.Domain.Entities;

namespace UnitTests.Domain;

public class CarEntityTests
{
    [Fact]
    public void Should_Create_New_Car()
    {
        var car = new Car(
            plateNumber: "ABC-123",
            brand: "Volvo",
            model: "V40",
            year: 2003
        );

        Assert.Equal("ABC-123", car.PlateNumber);
        Assert.Equal("Volvo", car.Brand);
        Assert.Equal("V40", car.Model);
        Assert.Equal(2003, car.Year);
    }

    [Fact]
    public void Should_Update_Owner()
    {
        var car = new Car("AAA-111", "Volvo", "V40",2003);

        car.AssignOwner(clientId: Guid.NewGuid());

        Assert.NotNull(car.OwnerId);
    }
}
