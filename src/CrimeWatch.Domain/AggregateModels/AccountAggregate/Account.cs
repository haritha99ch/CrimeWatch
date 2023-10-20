namespace CrimeWatch.Domain.AggregateModels.AccountAggregate;
public class Account : AggregateRoot<AccountId>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsModerator { get; set; }

    public static Account? Create(string email, string password, bool isModerator) => new()
    {
        Id = new(Guid.NewGuid()),
        Email = email,
        Password = password,
        IsModerator = isModerator
    };
}
