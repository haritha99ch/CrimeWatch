namespace CrimeWatch.Domain.Entities;
public class Witness : Entity<WitnessId>
{
    public UserId UserId { get; set; } = default!;
    public AccountId AccountId { get; set; } = default!;

    public Account? Account { get; set; }
    public User? User { get; set; }

    public static Witness Create(
            string firstName,
            string lastName,
            Gender gender,
            DateTime dateOfBirth,
            string phoneNumber,
            string email,
            string password
        )
    {
        var user = User.Create(firstName, lastName, gender, dateOfBirth, phoneNumber);
        var account = Account.Create(email, password, false);

        return new()
        {
            Id = new(Guid.NewGuid()),
            User = user,
            Account = account
        };
    }

    public Witness Update(
        string firstName,
        string lastName,
        Gender gender,
        DateTime dateOfBirth,
        string email,
        string password,
        string phoneNumber)
    {
        User!.FirstName = firstName;
        User!.LastName = lastName;
        User!.DateOfBirth = dateOfBirth;
        User!.PhoneNumber = phoneNumber;
        User!.Gender = gender;
        Account!.Email = email;
        Account!.Password = password;

        return this;
    }
}
