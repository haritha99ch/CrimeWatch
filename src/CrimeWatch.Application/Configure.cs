using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Application.Services;
using CrimeWatch.AppSettings;
using CrimeWatch.AppSettings.Options;
using CrimeWatch.Infrastructure;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace CrimeWatch.Application;
public static class Configure
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddInfrastructure();
        services.AddBlobService();
        services.AddTransient<IFileStorageService, BlobStorageService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddCqrs();
    }

    public static void AddApplication(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
    {
        services.AddInfrastructure(options);
        services.AddCqrs();
    }

    private static void AddCqrs(this IServiceCollection services)
        => services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AssemblyReference>());

    private static void AddBlobService(this IServiceCollection services)
    {
        var blobStorageOptions = services.GetOptions<BlobStorageOptions>();
        services.AddAzureClients(builder => builder.AddBlobServiceClient(blobStorageOptions.ConnectionString));
    }
}
