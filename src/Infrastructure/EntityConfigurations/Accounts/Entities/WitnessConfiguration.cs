using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Accounts.Entities;
internal static class WitnessConfiguration
{
    internal static void Configure(this OwnedNavigationBuilder<Account, Witness> builder)
    {
        builder.ToTable(nameof(Witness));

        builder.HasIndex(w => w.Id);
        builder.Property(w => w.Id).HasConversion(id => id.Value, id => new(id));

        builder.Property(w => w.CreatedAt).IsRequired();

        builder.Property(w => w.UpdatedAt);
    }
}
