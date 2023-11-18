using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.Entities;
using Infrastructure.ModelConfigurations.AccountAggregate.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations.AccountAggregate;
sealed internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{

    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion(id => id.Value, value => new(value));

        builder.Property(a => a.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(a => a.Email).IsUnique();

        builder.Property(a => a.Password).IsRequired().HasMaxLength(25);

        builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.HasIndex(a => a.PhoneNumber).IsUnique();

        builder.OwnsOne<Person>(e => e.Person, navigationBuilder => navigationBuilder.Configure());

        builder.OwnsOne<Witness>(e => e.Witness, navigationBuilder => navigationBuilder.Configure());

        builder.OwnsOne<Moderator>(e => e.Moderator, navigationBuilder => navigationBuilder.Configure());

        builder.Property(a => a.CreatedAt).IsRequired();

        builder.Property(a => a.UpdatedAt);
    }
}
