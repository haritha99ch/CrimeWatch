namespace CrimeWatch.Infrastructure.EntityConfigurations;
internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasConversion(e => e.Value, value => new(value));

        builder.HasIndex(a => a.Email).IsUnique();
        builder.Property(a => a.Email).IsRequired().HasMaxLength(100);

        builder.Property(a => a.Password).IsRequired().HasMaxLength(256);
    }
}
