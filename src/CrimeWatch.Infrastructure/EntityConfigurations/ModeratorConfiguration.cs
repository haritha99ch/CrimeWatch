namespace CrimeWatch.Infrastructure.EntityConfigurations;
internal class ModeratorConfiguration : IEntityTypeConfiguration<Moderator>
{
    public void Configure(EntityTypeBuilder<Moderator> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasConversion(e => e.Value, value => new(value));

        builder.HasOne(e => e.Account).WithOne().HasForeignKey<Moderator>(w => w.AccountId);
        builder.Property(e => e.AccountId).HasConversion(e => e.Value, value => new(value));

        builder.HasOne(e => e.User).WithOne().HasForeignKey<Moderator>(w => w.UserId);
        builder.Property(e => e.UserId).HasConversion(e => e.Value, value => new(value));

        builder.Property(e => e.PoliceId).HasMaxLength(50).IsRequired();
        builder.HasIndex(e => e.PoliceId).IsUnique();

        builder.Property(e => e.Province).HasMaxLength(50).IsRequired();
    }
}
