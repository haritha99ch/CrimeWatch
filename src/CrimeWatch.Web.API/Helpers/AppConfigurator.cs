using CrimeWatch.AppSettings.Options;
using CrimeWatch.AppSettings.Primitives;
using CrimeWatch.Web.API.OptionsConfigurations;

namespace CrimeWatch.Web.API.Helpers;
public static class AppConfigurator
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public static void ConfigureOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureOptions<JwtOptions>>();
        services.ConfigureOptions<JwtBearerOptionsConfiguration>();
        services.ConfigureOptions<ConfigureOptions<SqlServerOptions>>();
        services.ConfigureOptions<ConfigureOptions<BlobStorageOptions>>();
    }
}
