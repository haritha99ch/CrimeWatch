using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations.AccountAggregate.ValueObjectConfigurations;

internal static class PhoneNumberVerificationCodeConfiguration
{
    internal static void Configure(
        this OwnedNavigationBuilder<Account, PhoneNumberVerificationCode> builder
    )
    {
        builder.Property(e => e.Value).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
