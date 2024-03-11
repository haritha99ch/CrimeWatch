using Application;
using ApplicationSettings;
using Infrastructure;
using Persistence;

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
    }
}
