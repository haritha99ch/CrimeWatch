namespace CrimeWatch.Infrastructure.EntityConfigurations;
internal class EvidenceConfiguration : IEntityTypeConfiguration<Evidence>
{
    public void Configure(EntityTypeBuilder<Evidence> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasConversion(e => e.Value, e => new EvidenceId(e));

        builder.HasOne(e => e.Witness).WithOne().HasForeignKey<Evidence>(w => w.WitnessId);
        builder.Property(e => e.WitnessId).HasConversion(e => e.Value, value => new(value));

        builder.HasOne(e => e.Moderator).WithOne().HasForeignKey<Evidence>(w => w.ModeratorId);
        builder.Property(e => e.ModeratorId).HasConversion(e => e.Value, value => new(value));

        builder.HasOne(e => e.Report).WithOne().HasForeignKey<Evidence>(w => w.ReportId);
        builder.Property(e => e.ReportId).HasConversion(e => e.Value, value => new(value));

        builder.Property(e => e.Caption).IsRequired();

        builder.Property(e => e.Description).IsRequired();

        builder.Property(e => e.DateTime).IsRequired();

        builder.Property(e => e.Location).HasJsonPropertyName<Location>(nameof(Location));

        builder.Property(e => e.Status).IsRequired();

        builder.Property(e => e.ModeratorComment);

        builder.HasMany(e => e.MediaItems);
    }
}
