using Domain.AggregateModels.ReportAggregate;
using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Infrastructure.ModelConfigurations.ReportAggregate.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations.ReportAggregate;

internal sealed class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasConversion(id => id.Value, value => new(value));

        builder.Property(r => r.Caption).IsRequired();

        builder.Property(r => r.Description).IsRequired();

        builder.OwnsOne<Location>(
            r => r.Location,
            navigationBuilder => navigationBuilder.Configure()
        );

        builder.Property(r => r.Status).IsRequired();

        builder.Property(r => r.BookmarksCount).IsRequired().HasDefaultValue(0);

        builder
            .OwnsOne(r => r.MediaItem, navigationBuilder => navigationBuilder.Configure())
            .Navigation(r => r.MediaItem)
            .AutoInclude(false);

        builder
            .OwnsMany(r => r.Bookmarks, navigationBuilder => navigationBuilder.Configure())
            .Navigation(r => r.Bookmarks)
            .AutoInclude(false);

        builder
            .OwnsMany(r => r.Evidences, navigationBuilder => navigationBuilder.Configure())
            .Navigation(r => r.Evidences)
            .AutoInclude(false);

        builder
            .OwnsMany(e => e.Comments, navigationBuilder => navigationBuilder.Configure())
            .Navigation(r => r.Comments)
            .AutoInclude(false);

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
