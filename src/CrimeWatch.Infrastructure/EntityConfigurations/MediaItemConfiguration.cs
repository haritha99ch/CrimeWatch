namespace CrimeWatch.Infrastructure.EntityConfigurations;
internal class MediaItemConfiguration : IEntityTypeConfiguration<MediaItem>
{
    public void Configure(EntityTypeBuilder<MediaItem> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasConversion(e => e.Value, value => new(value));

        builder.Property(e => e.Url).IsRequired();

        builder.Property(e => e.Type).IsRequired();
    }
}
