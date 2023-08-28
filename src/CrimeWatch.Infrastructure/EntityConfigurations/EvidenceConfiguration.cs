namespace CrimeWatch.Infrastructure.EntityConfigurations;
internal class EvidenceConfiguration : IEntityTypeConfiguration<Evidence>
{
    public void Configure(EntityTypeBuilder<Evidence> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasConversion(e => e.Value, e => new EvidenceId(e));

        builder.HasOne(e => e.Witness).WithOne().HasForeignKey<Evidence>(w => w.WitnessId).OnDelete(DeleteBehavior.NoAction);
        builder.Property(e => e.WitnessId).HasConversion(e => e.Value, value => new(value));

        builder.HasOne(e => e.Moderator).WithOne().HasForeignKey<Evidence>(w => w.ModeratorId).OnDelete(DeleteBehavior.NoAction);
        builder.Property(e => e.ModeratorId)
            .HasConversion(e => e != null ? e.Value : default, value => value != default ? new(value) : null);


        builder.Property(e => e.Caption).IsRequired();

        builder.Property(e => e.Description).IsRequired();

        builder.Property(e => e.DateTime).IsRequired();

        builder.OwnsOne(e => e.Location, ownedNavigationBuilder => ownedNavigationBuilder.ToJson());

        builder.Property(e => e.Status).IsRequired();

        builder.Property(e => e.ModeratorComment);

        builder.HasMany(e => e.MediaItems);
    }
}
