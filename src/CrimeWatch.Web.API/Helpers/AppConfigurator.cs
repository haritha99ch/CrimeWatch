using CrimeWatch.Application;
using CrimeWatch.Web.API.Middleware;
using CrimeWatch.Web.API.Options;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace CrimeWatch.Web.API.Helpers;
public static class AppConfigurator
{
    private static T GetOptions<T>(this IServiceCollection services) where T : class
    {
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetService<IConfigureOptions<T>>();

        if (options != null) return serviceProvider.GetRequiredService<IOptions<T>>().Value;
        throw new InvalidOperationException(
            $"No configuration found for {typeof(T).Name}. "
            + $"Please ensure that services.ConfigureOptions<ConfigureOptions<{typeof(T).Name}>>() "
            + $"is called in {Assembly.GetCallingAssembly().GetName().Name}.");
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Validations
        services.AddTransient<ValidationExceptionHandlingMiddleware>();
        var appOptions = services.GetOptions<AppOptions>();
        if (appOptions.Validations) services.AddApplicationValidators();
    }

    public static void ConfigureOptions(this IServiceCollection services)
    {
        services.AddApplicationOptions();
        services.ConfigureOptions<ConfigureOptions<AppOptions>>();
    }
}
