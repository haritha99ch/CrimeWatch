using ApplicationSettings;
using ApplicationSettings.Helpers;
using ApplicationSettings.Options;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class Configure
{
    /// <summary>
    ///     Must call <see cref="ApplicationSettings.Configure.AddApplicationSettings" /> before calling this.
    /// </summary>
    /// <param name="serviceCollection"></param>
    public static void AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureSqlDbContext();
    }

    /// <summary>
    ///     Registers Sql server options from user secrets and appsettings.json
    ///     Registers <see cref="ApplicationDbContext" /> consuming sql options.
    /// </summary>
    /// <param name="serviceCollection"></param>
    private static void ConfigureSqlDbContext(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureSqlServerOptions();
        serviceCollection.AddDbContext<ApplicationDbContext>((serviceProvider, builder) =>
        {
            var sqlServerOptions = serviceProvider.GetRequiredOptions<SqlServerOptions>();
            builder.UseSqlServer(sqlServerOptions.ConnectionString, options =>
            {
                options.EnableRetryOnFailure(sqlServerOptions.MaxRetryCount);
                options.CommandTimeout(sqlServerOptions.CommandTimeout);
            });
            builder.EnableSensitiveDataLogging(sqlServerOptions.EnableSensitiveDataLogging);
            builder.EnableDetailedErrors(sqlServerOptions.EnableDetailedErrors);
        });
    }
}
