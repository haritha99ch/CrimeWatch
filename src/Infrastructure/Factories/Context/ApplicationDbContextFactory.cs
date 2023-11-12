using ApplicationSettings;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Factories.Context;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{

    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((_, config) => config.AddApplicationSettings())
            .ConfigureServices((_, services) => services.AddInfrastructure()).Build();
        return host.Services.GetRequiredService<ApplicationDbContext>();
    }
}
