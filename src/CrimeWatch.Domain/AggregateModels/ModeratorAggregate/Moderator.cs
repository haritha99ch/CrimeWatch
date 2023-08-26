using CrimeWatch.Domain.AggregateModels.AccountAggregate;
using CrimeWatch.Domain.AggregateModels.UserAggregate;

namespace CrimeWatch.Domain.AggregateModels.ModeratorAggregate;
public class Moderator : AggregateRoot<ModeratorId>
{
    public string PoliceId { get; set; } = string.Empty;
    public UserId UserId { get; set; } = default!;
    public AccountId AccountId { get; set; } = default!;
    public string Province { get; set; } = string.Empty;

    public User? User { get; set; }
    public Account? Account { get; set; }

    public static Moderator Create(
        string firstName,
        string lastName,
        Gender gender,
        DateOnly dateOfBirth,
        string phoneNumber,
        string policeId,
        string province,
        string email,
        string password
        )
    {
        User user = User.Create(firstName, lastName, gender, dateOfBirth, phoneNumber);
        Account account = Account.Create(email, password, true);

        return new Moderator
        {
            Id = new(Guid.NewGuid()),
            User = user,
            Account = account,
            PoliceId = policeId,
            Province = province
        };
    }
}
