using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace ApplicationSettings.Helpers;
public static class OptionsLocator
{
    public static T GetRequiredOptions<T>(this IServiceCollection serviceCollection)
        where T : class, IApplicationOptions
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        return serviceProvider.GetRequiredOptions<T>();
    }

    public static T GetRequiredOptions<T>(this IServiceProvider serviceProvider) where T : class, IApplicationOptions
    {
        var options = serviceProvider.GetService<IOptions<T>>()?.Value;

        if (options != null) return options;
        throw new InvalidOperationException(
            $"No configuration found for {typeof(T).Name}. "
            + $"Please ensure that services.ConfigureOptions<ConfigureApplicationOptions<{typeof(T).Name}>>() "
            + $"is called in {Assembly.GetCallingAssembly().GetName().Name}.");
    }
}
