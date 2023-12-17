using Domain.AggregateModels.ReportAggregate;
using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations.ReportAggregate.EntityConfiguration;
internal static class EvidenceConfiguration
{
    internal static void Configure(this OwnedNavigationBuilder<Report, Evidence> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasConversion(id => id.Value, value => new(value));

        builder.Property(e => e.Caption).IsRequired();

        builder.Property(e => e.Description).IsRequired();

        builder.OwnsOne<Location>(
            e => e.Location,
            navigationBuilder => navigationBuilder.Configure());

        builder.Property(e => e.Status).IsRequired();

        builder.OwnsMany(e => e.MediaItems, navigationBuilder => navigationBuilder.Configure());

        builder.OwnsMany(e => e.Comments, navigationBuilder => navigationBuilder.Configure());

        builder
            .HasOne(e => e.Author)
            .WithMany()
            .HasForeignKey(e => e.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Property(e => e.AuthorId)
            .HasConversion(id => id != null ? id.Value : default, value => new(value));

        builder
            .HasOne(e => e.Moderator)
            .WithMany()
            .HasForeignKey(e => e.ModeratorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Property(e => e.ModeratorId)
            .HasConversion(id => id != null ? id.Value : default, value => new(value));
    }
}
