using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Accounts.ValueObjects;
internal static class EmailVerificationCodeConfiguration
{
    internal static void Configure(
            this OwnedNavigationBuilder<Account, EmailVerificationCode> builder
        )
    {
        builder.Property(e => e.Value).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
