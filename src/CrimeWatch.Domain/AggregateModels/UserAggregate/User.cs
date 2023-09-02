namespace CrimeWatch.Domain.AggregateModels.UserAggregate;
public class User : AggregateRoot<UserId>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Age => DateTime.Now.Year - DateOfBirth.Year;
    public string PhoneNumber { get; set; } = string.Empty;

    public static User Create(string firstName, string lastName, Gender gender, DateTime dateOfBirth, string phoneNumber)
    {
        return new()
        {
            Id = new(Guid.NewGuid()),
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            PhoneNumber = phoneNumber
        };
    }
}