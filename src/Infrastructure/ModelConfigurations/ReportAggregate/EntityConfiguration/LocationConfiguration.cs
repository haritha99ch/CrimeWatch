using Domain.AggregateModels.ReportAggregate;
using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.ModelConfigurations.ReportAggregate.EntityConfiguration;
internal static class LocationConfiguration
{
    internal static void Configure(this OwnedNavigationBuilder<Report, Location> builder)
    {
        builder.ToJson();

        builder.Property(l => l.No);

        builder.Property(l => l.Street1).IsRequired();

        builder.Property(l => l.Street2);

        builder.Property(l => l.City).IsRequired();

        builder.Property(l => l.Province).IsRequired();
    }

    internal static void Configure(this OwnedNavigationBuilder<Evidence, Location> builder)
    {
        builder.Property(l => l.No);

        builder.Property(l => l.Street1).IsRequired();

        builder.Property(l => l.Street2);

        builder.Property(l => l.City).IsRequired();

        builder.Property(l => l.Province).IsRequired();
    }
}
