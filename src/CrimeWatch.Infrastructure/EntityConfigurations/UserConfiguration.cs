namespace CrimeWatch.Infrastructure.EntityConfigurations;
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasConversion(e => e.Value, value => new(value));

        builder.Property(e => e.FirstName).HasMaxLength(50).IsRequired();

        builder.Property(e => e.LastName).HasMaxLength(50).IsRequired();

        builder.Property(e => e.DateOfBirth).IsRequired();

        builder.Property(e => e.PhoneNumber).HasMaxLength(20).IsRequired();
    }
}
