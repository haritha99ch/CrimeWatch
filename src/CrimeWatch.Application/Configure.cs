using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Application.Services;
using CrimeWatch.Infrastructure;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace CrimeWatch.Application;
public static class Configure
{
    public static void AddApplication(this IServiceCollection services, string dbConnectionString, string storageConnectionString)
    {
        services.AddInfrastructure(dbConnectionString);
        services.AddAzureClients(builder => builder.AddBlobServiceClient(storageConnectionString));
        services.AddTransient<IFileStorageService, BlobStorageService>();
        services.AddCQRS();
    }

    public static void AddApplication(this IServiceCollection services, string dbConnectionString)
    {
        services.AddInfrastructure(dbConnectionString);
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
