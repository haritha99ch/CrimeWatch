using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.ReportAggregate;
using Domain.AggregateModels.ReportAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations.ReportAggregate.EntityConfiguration;

internal static class CommentConfiguration
{
    internal static void Configure(this OwnedNavigationBuilder<Report, Comment> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(id => id.Value, value => new(value));

        builder
            .HasOne<Account>(b => b.Account)
            .WithMany()
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.Content).IsRequired();

        builder.Property(c => c.CreatedAt).IsRequired();

        builder.Property(c => c.UpdatedAt);
    }

    internal static void Configure(this OwnedNavigationBuilder<Evidence, Comment> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(id => id.Value, value => new(value));

        builder
            .HasOne<Account>(b => b.Account)
            .WithMany()
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.Content).IsRequired();

        builder.Property(c => c.CreatedAt).IsRequired();

        builder.Property(c => c.UpdatedAt);
    }
}
