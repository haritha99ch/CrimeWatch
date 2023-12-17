using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.ReportAggregate;
using Domain.AggregateModels.ReportAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations.ReportAggregate.EntityConfiguration;
internal static class BookmarkConfiguration
{
    internal static void Configure(this OwnedNavigationBuilder<Report, Bookmark> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).HasConversion(id => id.Value, value => new(value));

        builder.Property(b => b.AccountId).HasConversion(id => id.Value, value => new(value));

        builder
            .HasOne<Account>(b => b.Account)
            .WithMany()
            .HasForeignKey(b => b.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
