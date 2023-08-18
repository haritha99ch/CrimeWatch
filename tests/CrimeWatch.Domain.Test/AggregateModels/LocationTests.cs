using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Domain.Test.AggregateModels;
[TestClass]
public class LocationTests
{
    [TestMethod]
    public void Location_Create_ReturnsValidLocation()
    {
        // Arrange
        string no = "123";
        string street1 = "First Street";
        string street2 = "Second Ave";
        string city = "Townsville";
        string province = "Stateville";

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