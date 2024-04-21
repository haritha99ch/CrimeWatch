using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Accounts.Entities;
internal static class ModeratorConfiguration
{
    internal static void Configure(this OwnedNavigationBuilder<Account, Moderator> builder)
    {
        builder.ToTable(nameof(Moderator));

        builder.HasIndex(m => m.Id);
        builder.Property(m => m.Id).HasConversion(id => id.Value, id => new(id));

        builder.Property(m => m.PoliceId).IsRequired().HasMaxLength(20);
        builder.HasIndex(m => m.PoliceId).IsUnique();

        builder.Property(m => m.City).IsRequired().HasMaxLength(100);

        builder.Property(m => m.Province).IsRequired().HasMaxLength(100);

        builder.Property(m => m.CreatedAt).IsRequired();

        builder.Property(m => m.UpdatedAt);
    }
}
