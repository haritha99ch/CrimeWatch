using CrimeWatch.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CrimeWatch.Application;
public static class Configure
{
    public static void AddApplication(this IServiceCollection services, string connectionString)
    {
        services.AddInfrastructure(connectionString);
        services.AddCQRS();
    }

    public static void AddApplication(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
    {
        services.AddInfrastructure(options);
        services.AddCQRS();
    }

    private static void AddCQRS(this IServiceCollection services)
        => services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AssemblyReference>());
}
