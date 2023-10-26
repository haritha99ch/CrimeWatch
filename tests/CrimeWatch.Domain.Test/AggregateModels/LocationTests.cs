namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class LocationTests
{
    [TestMethod]
    public void LocationCreate_ReturnsValidLocation()
    {
        // Arrange
        var no = "123";
        var street1 = "First Street";
        var street2 = "Second Ave";
        var city = "Townsville";
        var province = "Stateville";

        // Act
        Location location = Location.Create(no, street1, street2, city, province);

        // Assert
        Assert.IsNotNull(location);
        Assert.AreEqual(no, location.No);
        Assert.AreEqual(street1, location.Street1);
        Assert.AreEqual(street2, location.Street2);
        Assert.AreEqual(city, location.City);
        Assert.AreEqual(province, location.Province);
    }
}
