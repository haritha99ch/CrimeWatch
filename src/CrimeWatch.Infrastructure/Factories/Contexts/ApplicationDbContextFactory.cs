namespace CrimeWatch.Infrastructure.Factories.Contexts;
public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ApplicationDbContext CreateDbContext()
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

        DbContextOptionsBuilder<ApplicationDbContext> dbBuilder = new();
        dbBuilder.UseSqlServer(connectionString);
        return new ApplicationDbContext(dbBuilder.Options);
    }
}

