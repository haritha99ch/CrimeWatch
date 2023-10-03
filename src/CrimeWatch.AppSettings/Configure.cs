using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace CrimeWatch.AppSettings;
public static class Configure
{
    public static void AddAppSettings(this IConfigurationBuilder configuration)
    {
        configuration.AddUserSecrets<AssemblyReference>();
        configuration.AddJsonFile(path: "appsettings.json", false, true);
    }

    public static T GetOptions<T>(this IServiceCollection services) where T : class
    {
        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetOptions<T>();
    }

    public static T GetOptions<T>(this IServiceProvider serviceProvider) where T : class
    {
        var options = serviceProvider.GetService<IOptions<T>>()?.Value;

        if (options != null) return options;
        throw new InvalidOperationException(
            $"No configuration found for {typeof(T).Name}. "
            + $"Please ensure that services.ConfigureOptions<ConfigureOptions<{typeof(T).Name}>>() "
            + $"is called in {Assembly.GetCallingAssembly().GetName().Name}.");
    }
}
