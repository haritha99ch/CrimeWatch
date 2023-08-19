﻿using CrimeWatch.Domain.AggregateModels.AccountAggregate;
using CrimeWatch.Domain.AggregateModels.UserAggregate;

namespace CrimeWatch.Domain.AggregateModels.WitnessAggregate;
public class Witness : AggregateRoot<WitnessId>
{
    public UserId UserId { get; set; } = default!;
    public AccountId AccountId { get; set; } = default!;

    public Account? Account { get; set; }
    public User? User { get; set; }

    public static Witness Create(
        string firstName,
        string lastName,
        Gender gender,
        DateOnly dateOfBirth,
        string phoneNumber,
        string email,
        string password
        )
    {
        User user = User.Create(firstName, lastName, gender, dateOfBirth, phoneNumber);
        Account account = Account.Create(email, password, false);

        return new()
        {
            Id = new(new()),
            User = user,
            Account = account
        };
    }
}