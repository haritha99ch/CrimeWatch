namespace CrimeWatch.Domain.AggregateModels.AccountAggregate;
public class Account : AggregateRoot<AccountId>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsModerator { get; set; } = false;

    public static Account Create(string email, string password, bool isModerator)
    {
        return new()
        {
            Id = new(new()),
            Email = email,
            Password = password,
            IsModerator = isModerator
        };
    }
}
