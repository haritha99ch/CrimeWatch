namespace Domain.Test.AggregateModels.AccountAggregateTests;
public class WhenUpdatingAccount
{
    [TestMethod]
    public void Should_UpdateWitnessAccount()
    {
        var account = DataProvider.TestAccountForWitness;
        var person = account.Person!;

        var newLastName = DataProvider.LastName;

        account.UpdateWitness(
            person.Nic,
            person.FirstName,
            newLastName,
            person.Gender,
            person.BirthDate);

        Assert.IsNotNull(account.Person);
        person = account.Person;
        Assert.AreEqual(newLastName, person.LastName);
    }

    [TestMethod]
    public void ShouldRaise_AccountUpdated_When_Updating_Account_For_Witness()
    {
        var account = DataProvider.TestAccountForWitness;
        var person = account.Person!;

        var newLastName = DataProvider.LastName;

        account.UpdateWitness(
            person.Nic,
            person.FirstName,
            newLastName,
            person.Gender,
            person.BirthDate);

        Assert.IsTrue(account.HasDomainEvent<AccountUpdatedEvent>());
    }

    [TestMethod]
    public void Should_UpdateModeratorAccount()
    {
        var account = DataProvider.TestAccountForModerator;
        var person = account.Person!;
        var moderator = account.Moderator!;

        var newLastName = DataProvider.LastName;

        account.UpdateModerator(
            person.Nic,
            person.FirstName,
            newLastName,
            person.Gender,
            person.BirthDate,
            moderator.PoliceId,
            moderator.City,
            moderator.Province);

        Assert.IsNotNull(account.Person);
        person = account.Person;
        Assert.AreEqual(newLastName, person.LastName);

        Assert.IsNotNull(account.Moderator);
        moderator = account.Moderator;
        Assert.AreEqual(moderator.City, moderator.City);
        Assert.AreEqual(moderator.Province, moderator.Province);
    }

    [TestMethod]
    public void ShouldRaise_AccountUpdated_When_Updating_Account_For_Moderator()
    {
        var account = DataProvider.TestAccountForModerator;
        var person = account.Person!;
        var moderator = account.Moderator!;

        var newLastName = DataProvider.LastName;

        account.UpdateModerator(
            person.Nic,
            person.FirstName,
            newLastName,
            person.Gender,
            person.BirthDate,
            moderator.PoliceId,
            moderator.City,
            moderator.Province);

        Assert.IsTrue(account.HasDomainEvent<AccountUpdatedEvent>());
    }
}
