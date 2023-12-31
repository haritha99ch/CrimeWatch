﻿using CrimeWatch.Domain.AggregateModels.AccountAggregate;
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

    public static Moderator? Create(
            string firstName,
            string lastName,
            Gender gender,
            DateTime dateOfBirth,
            string phoneNumber,
            string policeId,
            string province,
            string email,
            string password
        )
    {
        var user = User.Create(firstName, lastName, gender, dateOfBirth, phoneNumber);
        var account = Account.Create(email, password, true);

        return new()
        {
            Id = new(Guid.NewGuid()),
            User = user,
            Account = account,
            PoliceId = policeId,
            Province = province
        };
    }

    public Moderator Update(
        string firstName,
        string lastName,
        Gender gender,
        DateTime dateOfBirth,
        string phoneNumber,
        string policeId,
        string province,
        string email,
        string password)
    {
        User!.FirstName = firstName;
        User!.LastName = lastName;
        User!.DateOfBirth = dateOfBirth;
        User!.PhoneNumber = phoneNumber;
        User!.Gender = gender;
        Account!.Email = email;
        Account!.Password = password;
        PoliceId = policeId;
        Province = province;

        return this;
    }
}
