using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations.AccountAggregate.EntityConfigurations;
internal static class PersonConfiguration
{

    internal static void Configure(this OwnedNavigationBuilder<Account, Person> builder)
    {
        builder.ToTable(nameof(Person));

        builder.HasIndex(p => p.Id);
        builder.Property(p => p.Id).HasConversion(id => id.Value, id => new(id));

        builder.Property(p => p.Nic).IsRequired().HasMaxLength(20);
        builder.HasIndex(p => p.Nic).IsUnique();

        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(50);

        builder.Property(p => p.LastName).IsRequired().HasMaxLength(50);

        builder.Property(p => p.Gender).IsRequired();

        builder.Property(p => p.BirthDate).IsRequired();

        builder.Property(p => p.CreatedAt).IsRequired();

        builder.Property(p => p.UpdatedAt);
    }
}
