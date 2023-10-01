using CrimeWatch.Application;
using CrimeWatch.AppSettings.Options;
using CrimeWatch.AppSettings.Primitives;
using CrimeWatch.Web.API.Middleware;
using CrimeWatch.Web.API.OptionsConfigurations;
using FluentValidation;

namespace CrimeWatch.Web.API.Helpers;
public static class AppConfigurator
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddValidatorsFromAssemblyContaining<AssemblyReference>();
        services.AddTransient<ValidationExceptionHandlingMiddleware>();
    }

    public static void ConfigureOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureOptions<JwtOptions>>();
        services.ConfigureOptions<JwtBearerOptionsConfiguration>();
        services.ConfigureOptions<ConfigureOptions<SqlServerOptions>>();
        services.ConfigureOptions<ConfigureOptions<BlobStorageOptions>>();
    }
}
