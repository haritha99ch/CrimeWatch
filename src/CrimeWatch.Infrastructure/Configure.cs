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
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(UseSqlServerFromOptions);
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

    private static void UseSqlServerFromOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder builder)
    {
        var sqlServerOptions = serviceProvider.GetOptions<SqlServerOptions>();
        builder.UseSqlServer(sqlServerOptions.ConnectionString, options =>
        {
            options.EnableRetryOnFailure(sqlServerOptions.MaxRetryCount);
            options.CommandTimeout(sqlServerOptions.CommandTimeout);
        });
        builder.EnableSensitiveDataLogging(sqlServerOptions.EnableSensitiveDataLogging);
        builder.EnableDetailedErrors(sqlServerOptions.EnableDetailedErrors);
    }
}
