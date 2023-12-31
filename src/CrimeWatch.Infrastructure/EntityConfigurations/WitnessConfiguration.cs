﻿namespace CrimeWatch.Infrastructure.EntityConfigurations;
internal class WitnessConfiguration : IEntityTypeConfiguration<Witness>
{
    public void Configure(EntityTypeBuilder<Witness> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasConversion(e => e.Value, value => new(value));

        builder.HasOne(e => e.Account).WithOne().HasForeignKey<Witness>(w => w.AccountId);
        builder.Property(e => e.AccountId).HasConversion(e => e.Value, value => new(value));

        builder.HasOne(e => e.User).WithOne().HasForeignKey<Witness>(w => w.UserId);
        builder.Property(e => e.UserId).HasConversion(e => e.Value, value => new(value));
    }
}
