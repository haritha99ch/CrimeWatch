using ApplicationSettings;
using ApplicationSettings.Helpers;
using ApplicationSettings.Options;
using Azure.Storage.Blobs;
using Infrastructure.Context;
using Infrastructure.Contracts.Services;
using Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class Configure
{
    /// <summary>
    ///     Must call <see cref="ApplicationSettings.Configure.AddApplicationSettings" /> before calling this.
    /// </summary>
    /// <param name="serviceCollection"></param>
    public static void AddInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureSqlDbContext();
        serviceCollection.ConfigureBlobStorageClient();
    }

    public static void AddInfrastructure(
            this IServiceCollection serviceCollection,
            string instanceName
        )
    {
        serviceCollection.ConfigureSqlDbContext(instanceName);
        serviceCollection.ConfigureBlobStorageClient();
    }

    /// <summary>
    ///     Registers Sql server options from user secrets and appsettings.json
    ///     Registers <see cref="ApplicationDbContext" /> consuming sql options.
    /// </summary>
    /// <param name="serviceCollection"></param>
    private static void ConfigureSqlDbContext(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureSqlServerOptions();
        serviceCollection.AddDbContext<ApplicationDbContext>(
            (serviceProvider, builder) =>
            {
                var sqlServerOptions = serviceProvider.GetRequiredOptions<SqlServerOptions>();
                builder.UseSqlServer(
                    sqlServerOptions.ConnectionString,
                    options =>
                    {
                        options.EnableRetryOnFailure(sqlServerOptions.MaxRetryCount);
                        options.CommandTimeout(sqlServerOptions.CommandTimeout);
                    });
                builder.EnableSensitiveDataLogging(sqlServerOptions.EnableSensitiveDataLogging);
                builder.EnableDetailedErrors(sqlServerOptions.EnableDetailedErrors);
            });
    }

    private static void ConfigureSqlDbContext(
            this IServiceCollection serviceCollection,
            string instanceName
        )
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(
            builder =>
            {
                var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = @"(localdb)\MSSQLLocalDB",
                    InitialCatalog = $"crime-watch-db-test-{instanceName}",
                    IntegratedSecurity = true
                };
                builder.UseSqlServer(
                    sqlConnectionStringBuilder.ConnectionString,
                    options =>
                    {
                        options.EnableRetryOnFailure(5);
                        options.CommandTimeout(30);
                    });
                builder.EnableSensitiveDataLogging();
                builder.EnableDetailedErrors();
            });
    }

    private static void ConfigureBlobStorageClient(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureBlobStorageOptions();
        var blobStorageOptions = serviceCollection.GetRequiredOptions<BlobStorageOptions>();

        serviceCollection.AddSingleton(e => new BlobServiceClient(blobStorageOptions.ConnectionString,
            new()
            {
                Retry =
                {
                    MaxRetries = blobStorageOptions.MaximumRetryCount
                }
            }));
        serviceCollection.AddScoped<IBlobStorageClient, BlobStorageClient>();
    }
}
