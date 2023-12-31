﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace CrimeWatch.Infrastructure.EntityConfigurations;
internal class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasConversion(e => e.Value, e => new(e));

        builder.HasOne(e => e.Witness).WithMany().HasForeignKey(w => w.WitnessId).OnDelete(DeleteBehavior.NoAction);
        builder.Property(e => e.WitnessId).HasConversion(e => e.Value, value => new(value));

        builder.HasOne(e => e.Moderator).WithMany().HasForeignKey(w => w.ModeratorId).OnDelete(DeleteBehavior.NoAction);
        builder.Property(e => e.ModeratorId)
            .HasConversion(e => e != null ? e.Value : default, value => value != default ? new(value) : null);

        builder.Property(e => e.Title).IsRequired();

        builder.Property(e => e.Description).IsRequired();

        builder.Property(e => e.DateTime).IsRequired();

        builder.OwnsOne(e => e.Location, ownedNavigationBuilder => ownedNavigationBuilder.ToJson());

        builder.Property(e => e.Status).IsRequired();

        builder.Property(r => r.StaredBy)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<WitnessId>>(v)!)
            .Metadata.SetValueComparer(new ValueComparer<List<WitnessId>>(
                (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
                c => c != null ? c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())) : 0,
                c => new List<WitnessId>(c ?? new List<WitnessId>())));

        builder.Property(e => e.ModeratorComment);

        builder.HasOne(e => e.MediaItem);

        builder.HasMany(e => e.Evidences).WithOne().HasForeignKey(e => e.ReportId);
    }
}
