using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace CrimeWatch.AppSettings;
public static class Configure
{
    public static void AddAppSettings(this IConfigurationBuilder configuration)
        => configuration.AddUserSecrets<AssemblyReference>();

    public static T GetOptions<T>(this IServiceCollection services) where T : class
    {
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetService<IConfigureOptions<T>>();

        if (options != null) return serviceProvider.GetRequiredService<IOptions<T>>().Value;
        throw new InvalidOperationException(
            $"No configuration found for {typeof(T).Name}. "
            + $"Please ensure that services.ConfigureOptions<ConfigureOptions<{typeof(T).Name}>>() "
            + $"is called in {Assembly.GetCallingAssembly().GetName().Name}.");
    }
}
