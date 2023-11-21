using Domain.AggregateModels.AccountAggregate.Entities;
using Domain.AggregateModels.AccountAggregate.Enums;
using Domain.AggregateModels.AccountAggregate.Events;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using static BCrypt.Net.BCrypt;

namespace Domain.AggregateModels.AccountAggregate;

public sealed record Account : AggregateRoot<AccountId>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PhoneNumber { get; set; }
    public required AccountType AccountType { get; init; }
    public required EmailVerificationCode EmailVerificationCode { get; set; }
    public required PhoneNumberVerificationCode PhoneNumberVerificationCode { get; set; }
    public required bool IsEmailVerified { get; set; }
    public required bool IsPhoneNumberVerified { get; set; }

    public Person? Person { get; private init; }
    public Witness? Witness { get; private init; }
    public Moderator? Moderator { get; private init; }

    private static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public static Account CreateAccountForWitness(
        string nic,
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDay,
        string email,
        string password,
        string phoneNumber
    )
    {
        var account = new Account
        {
            Email = email,
            Password = HashPassword(password),
            PhoneNumber = phoneNumber,
            AccountType = AccountType.Witness,
            Id = new(Guid.NewGuid()),
            Person = Person.Create(nic, firstName, lastName, gender, birthDay),
            Witness = Witness.Create(),
            CreatedAt = DateTime.Now,
            IsEmailVerified = false,
            IsPhoneNumberVerified = false,
            EmailVerificationCode = EmailVerificationCode.Create(),
            PhoneNumberVerificationCode = PhoneNumberVerificationCode.Create()
        };
        account.RaiseDomainEvent(new AccountCreatedEvent(account));

        return account;
    }

    public static Account CreateAccountForModerator(
        string nic,
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDay,
        string policeId,
        string city,
        string province,
        string email,
        string password,
        string phoneNumber
    )
    {
        var account = new Account
        {
            Email = email,
            Password = HashPassword(password),
            PhoneNumber = phoneNumber,
            AccountType = AccountType.Moderator,
            Id = new(Guid.NewGuid()),
            Person = Person.Create(nic, firstName, lastName, gender, birthDay),
            Witness = Witness.Create(),
            Moderator = Moderator.Create(policeId, city, province),
            CreatedAt = DateTime.Now,
            IsEmailVerified = false,
            IsPhoneNumberVerified = false,
            EmailVerificationCode = EmailVerificationCode.Create(),
            PhoneNumberVerificationCode = PhoneNumberVerificationCode.Create()
        };

        account.RaiseDomainEvent(new AccountCreatedEvent(account));
        return account;
    }

    public void UpdateModerator(
        string nic,
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDay,
        string policeId,
        string city,
        string province
    )
    {
        var personUpdated = Person!.Update(nic, firstName, lastName, gender, birthDay);
        var moderatorUpdated = Moderator!.Update(policeId, city, province);

        if (!personUpdated && !moderatorUpdated)
            return;
        UpdatedAt = DateTime.Now;
        RaiseDomainEvent(new AccountUpdatedEvent(Id));
    }

    public void UpdateWitness(
        string nic,
        string firstName,
        string lastName,
        Gender gender,
        DateOnly birthDay
    )
    {
        var personUpdated = Person!.Update(nic, firstName, lastName, gender, birthDay);
        var witnessUpdated = Witness!.Update();

        UpdatedAt = Person.UpdatedAt > Witness.UpdatedAt ? Person.UpdatedAt : Witness.UpdatedAt;

        if (!personUpdated && !witnessUpdated)
            return;
        UpdatedAt = DateTime.Now;
        RaiseDomainEvent(new AccountUpdatedEvent(Id));
    }

    public void ChangeEmail(string newEmail)
    {
        if (Email.Equals(newEmail))
            throw new("New email must be different from the old email.");

        Email = newEmail;
        UpdatedAt = DateTime.Now;
        IsEmailVerified = false;
        EmailVerificationCode = EmailVerificationCode.Create();
        RaiseDomainEvent(new AccountEmailChangedEvent(Id, Email, EmailVerificationCode));
    }

    public void VerifyEmail(EmailVerificationCode verificationCode)
    {
        if (IsEmailVerified)
            return;
        if (EmailVerificationCode.IsExpired)
            throw new("Verification code has expired");
        if (!EmailVerificationCode.Equals(verificationCode))
            throw new("Invalid verification code.");

        IsEmailVerified = true;
        RaiseDomainEvent(new AccountEmailVerifiedEvent(Id, Email));
    }

    public void ChangePassword(string newPassword)
    {
        if (Verify(newPassword, Password))
            throw new("New password must be different from the old password.");

        Password = HashPassword(newPassword);
        UpdatedAt = DateTime.Now;
        RaiseDomainEvent(new AccountPasswordChangedEvent(Id));
    }

    public void ChangePhoneNumber(string newPhoneNumber)
    {
        if (PhoneNumber.Equals(newPhoneNumber))
            throw new("New phone number must be different from the old phone number.");

        PhoneNumber = newPhoneNumber;
        UpdatedAt = DateTime.Now;
        IsPhoneNumberVerified = false;
        PhoneNumberVerificationCode = PhoneNumberVerificationCode.Create();
        RaiseDomainEvent(
            new AccountPhoneNumberChangedEvent(Id, PhoneNumber, PhoneNumberVerificationCode)
        );
    }

    public void VerifyPhoneNumber(PhoneNumberVerificationCode verificationCode)
    {
        if (IsPhoneNumberVerified)
            return;
        if (PhoneNumberVerificationCode.IsExpired)
            throw new("Verification code has expired");
        if (!PhoneNumberVerificationCode.Equals(verificationCode))
            throw new("Invalid verification code.");

        IsPhoneNumberVerified = true;
        RaiseDomainEvent(new AccountPhoneNumberVerifiedEvent(Id, PhoneNumber));
    }

    public EmailVerificationCode RequestEmailVerificationCode()
    {
        EmailVerificationCode = EmailVerificationCode.Create();
        IsEmailVerified = false;
        RaiseDomainEvent(new AccountEmailVerificationCodeRequestedEvent(Id, EmailVerificationCode));
        return EmailVerificationCode;
    }

    public PhoneNumberVerificationCode RequestPhoneNumberVerificationCode()
    {
        PhoneNumberVerificationCode = PhoneNumberVerificationCode.Create();
        IsPhoneNumberVerified = false;
        RaiseDomainEvent(
            new AccountPhoneNumberVerificationCodeRequestedEvent(Id, PhoneNumberVerificationCode)
        );
        return PhoneNumberVerificationCode;
    }

    public bool VerifyPassword(string password) => Verify(password, Password);
}
