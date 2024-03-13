using Application;
using ApplicationSettings;
using Infrastructure;
using Persistence;
using Web.API.Middlewares;

namespace Web.API.Helpers;
internal static class AppConfigurator
{
    public static void ConfigureConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddApplicationSettings();
    }

    public static void ConfigureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddInfrastructure();
        serviceCollection.AddPersistence();
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddApplication();
        serviceCollection.AddTransient<ErrorHandlingMiddleWare>();
    }
}
