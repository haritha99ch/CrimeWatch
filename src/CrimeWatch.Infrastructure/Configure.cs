using CrimeWatch.AppSettings;
using CrimeWatch.AppSettings.Options;
using CrimeWatch.Infrastructure.Contracts.Repositories;
using CrimeWatch.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CrimeWatch.Infrastructure;
public static class Configure
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        var sqlServerOptions = services.GetOptions<SqlServerOptions>();
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
            options => options.UseSqlServer(sqlServerOptions.ConnectionString));
        services.AddRepositories();
    }

    public static void AddInfrastructure(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder>? optionsAction
        )
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(optionsAction);
        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
        => services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
}
