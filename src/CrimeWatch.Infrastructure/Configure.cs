using CrimeWatch.Infrastructure.Contracts.Repositories;
using CrimeWatch.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrimeWatch.Infrastructure;
public static class Configure
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
            options => options.UseSqlServer(connectionString));

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
    }
}
