using CrimeWatch.Infrastructure.Contracts.Repositories;
using CrimeWatch.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrimeWatch.Infrastructure;
public static class Configure
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        services.AddRepositories();
    }

    public static void AddInfrastructure(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(optionsAction);
        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
        => services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
}
