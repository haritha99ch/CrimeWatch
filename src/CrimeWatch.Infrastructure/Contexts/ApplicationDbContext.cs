using CrimeWatch.Domain.Entities;

namespace CrimeWatch.Infrastructure.Contexts;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Account> Account { get; set; } = default!;
    public DbSet<User> User { get; set; } = default!;
    public DbSet<Witness> Witness { get; set; } = default!;
    public DbSet<Moderator> Moderator { get; set; } = default!;
    public DbSet<Report> Report { get; set; } = default!;
    public DbSet<Evidence> Evidence { get; set; } = default!;
    public DbSet<MediaItem> MediaItem { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
