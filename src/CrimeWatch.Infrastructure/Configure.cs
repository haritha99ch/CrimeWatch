using CrimeWatch.Infrastructure.Contracts.Repositories;
using CrimeWatch.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrimeWatch.Infrastructure;
public static class Configure
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString, Action<DbContextOptionsBuilder>? optionsAction = null)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(optionsAction ??
            (options => options.UseSqlServer(connectionString)));

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
    }
}
