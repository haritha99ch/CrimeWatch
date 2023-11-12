using ApplicationSettings.Common.Options;
using ApplicationSettings.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationSettings;
public static class Configure
{
    /// <summary>
    ///     Adds user-secrets.
    ///     appsettings.json will not be added.
    /// </summary>
    /// <param name="configurationBuilder"></param>
    public static void AddApplicationSettings(this IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddUserSecrets<AssemblyReference>();
    }

    /// <summary>
    ///     Configures and registers <see cref="SqlServerOptions" />
    /// </summary>
    /// <param name="serviceCollection"></param>
    public static void ConfigureSqlServerOptions(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureOptions<ConfigureApplicationOptions<SqlServerOptions>>();
    }
}
