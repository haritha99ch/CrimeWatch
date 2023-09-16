using CrimeWatch.Web.API.OptionConfigurations;

namespace CrimeWatch.Web.API.Helpers;
public static class AppConfigurator
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    public static void ConfigureOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsConfiguration>();
        services.ConfigureOptions<JwtBearerOptionsConfiguration>();
    }
}
