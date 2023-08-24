using CrimeWatch.AppSettings;
using Microsoft.EntityFrameworkCore.Design;

namespace CrimeWatch.Infrastructure.Factories.Contexts;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        ConfigurationBuilder? builder = new();
        builder.AddAppSettings();
        IConfigurationRoot? configuration = builder.Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection")!;

        DbContextOptionsBuilder<ApplicationDbContext> dbBuilder = new();
        dbBuilder.UseSqlServer(connectionString);
        return new ApplicationDbContext(dbBuilder.Options);
    }
}

