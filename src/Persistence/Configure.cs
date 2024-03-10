using Microsoft.Extensions.DependencyInjection;
using Persistence.Contracts.Repositories;
using Persistence.Contracts.Services;
using Persistence.Repositories;
using Persistence.Services;

namespace Persistence;
public static class Configure
{
    /// <summary>
    ///     Required dependency: <see cref="Infrastructure.Configure.AddInfrastructure" />
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <summary>
    /// </summary>
    /// <param name="serviceCollection"></param>
    public static void AddPersistence(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        serviceCollection.AddScoped<IFileStorageService, AzureBlobStorageService>();
    }
}
