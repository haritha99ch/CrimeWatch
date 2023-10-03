using CrimeWatch.Application;
using CrimeWatch.AppSettings;
using CrimeWatch.AppSettings.Primitives;
using CrimeWatch.Web.API.Middlewares;
using CrimeWatch.Web.API.Options;

namespace CrimeWatch.Web.API.Helpers;
public static class AppConfigurator
{
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
