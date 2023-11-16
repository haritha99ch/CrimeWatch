using Domain.AggregateModels.ReportAggregate;
using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Infrastructure.ModelConfigurations.ReportAggregate.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations.ReportAggregate;
sealed internal class ReportConfiguration : IEntityTypeConfiguration<Report>
{

    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasConversion(id => id.Value, value => new(value));

        builder.Property(r => r.Caption).IsRequired();

        builder.Property(r => r.Description).IsRequired();

        builder.OwnsOne<Location>(r => r.Location, navigationBuilder => navigationBuilder.Configure());

        builder.Property(r => r.Status).IsRequired();

        builder.Property(r => r.BookmarksCount).IsRequired().HasDefaultValue(0);

        builder.OwnsOne(r => r.MediaItem, navigationBuilder => navigationBuilder.Configure());

        builder.OwnsMany(r => r.Bookmarks, navigationBuilder => navigationBuilder.Configure());

        builder.OwnsMany(r => r.Evidences, navigationBuilder => navigationBuilder.Configure());

        builder.OwnsMany(e => e.Comments, navigationBuilder => navigationBuilder.Configure());

        builder.HasOne(e => e.Author).WithMany().HasForeignKey(e => e.AuthorId);
        builder.Property(e => e.AuthorId)
            .HasConversion(id => id != null ? id.Value : default, value => new(value));

        builder.HasOne(e => e.Moderator).WithMany().HasForeignKey(e => e.ModeratorId);
        builder.Property(e => e.ModeratorId)
            .HasConversion(id => id != null ? id.Value : default, value => new(value));
    }
}
