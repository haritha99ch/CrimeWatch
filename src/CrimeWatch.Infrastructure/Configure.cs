using Microsoft.Extensions.DependencyInjection;

namespace CrimeWatch.Infrastructure;
public static class Configure
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
            options => options.UseSqlServer(connectionString));
    }
}
