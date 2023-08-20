namespace CrimeWatch.Infrastructure.Contexts;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Account> Account { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Witness> Witness { get; set; }
    public DbSet<Moderator> Moderator { get; set; }
    public DbSet<Report> Report { get; set; }
    public DbSet<Evidence> Evidence { get; set; }
    public DbSet<MediaItem> MediaItem { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
