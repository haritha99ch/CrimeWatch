﻿using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.Entities;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Infrastructure.EntityConfigurations.Accounts.Entities;
using Infrastructure.EntityConfigurations.Accounts.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Accounts;
internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion(id => id.Value, value => new(value));

        builder.Property(a => a.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(a => a.Email).IsUnique();

        builder.Property(a => a.Password).IsRequired();

        builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.HasIndex(a => a.PhoneNumber).IsUnique();

        builder.Property(a => a.IsEmailVerified).IsRequired();

        builder.Property(a => a.IsPhoneNumberVerified).IsRequired();

        builder
            .OwnsOne<EmailVerificationCode>(
                a => a.EmailVerificationCode,
                navigationBuilder => navigationBuilder.Configure())
            .Navigation(e => e.EmailVerificationCode)
            .AutoInclude(false);

        builder
            .OwnsOne<PhoneNumberVerificationCode>(
                a => a.PhoneNumberVerificationCode,
                navigationBuilder => navigationBuilder.Configure())
            .Navigation(e => e.PhoneNumberVerificationCode)
            .AutoInclude(false);

        builder
            .OwnsOne<Person>(e => e.Person, navigationBuilder => navigationBuilder.Configure())
            .Navigation(e => e.Person)
            .AutoInclude(false);

        builder
            .OwnsOne<Witness>(e => e.Witness, navigationBuilder => navigationBuilder.Configure())
            .Navigation(e => e.Witness)
            .AutoInclude(false);
        ;

        builder
            .OwnsOne<Moderator>(
                e => e.Moderator,
                navigationBuilder => navigationBuilder.Configure())
            .Navigation(e => e.Moderator)
            .AutoInclude(false);

        builder.Property(a => a.CreatedAt).IsRequired();

        builder.Property(a => a.UpdatedAt);
    }
}
