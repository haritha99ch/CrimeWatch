using Domain.AggregateModels.ReportAggregate;
using Domain.AggregateModels.ReportAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations.ReportAggregate.EntityConfiguration;

internal static class MediaItemConfiguration
{
    internal static void Configure(this OwnedNavigationBuilder<Report, MediaItem> builder)
    {
        builder.ToTable($"{nameof(Report)}{nameof(MediaItem)}");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasConversion(id => id.Value, value => new(value));

        builder.Property(m => m.Url).IsRequired();

        builder.Property(m => m.MediaType).IsRequired();

        builder.Property(m => m.CreatedAt).IsRequired();

        builder.Property(m => m.UpdatedAt);
    }

    internal static void Configure(this OwnedNavigationBuilder<Evidence, MediaItem> builder)
    {
        builder.ToTable($"{nameof(Evidence)}{nameof(MediaItem)}");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasConversion(id => id.Value, value => new(value));

        builder.Property(m => m.Url).IsRequired();

        builder.Property(m => m.MediaType).IsRequired();

        builder.Property(m => m.CreatedAt).IsRequired();

        builder.Property(m => m.UpdatedAt);
    }
}
