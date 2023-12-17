using Infrastructure;
using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Persistence.Test.Common.Host;
public class App
{
    private readonly IServiceScope _scope;
    private readonly IHost _host;
    public ApplicationDbContext DbContext => _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    private App()
    {
        _host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .ConfigureServices(
                (_, services) =>
                {
                    services.AddInfrastructure("persistence-test");
                    services.AddPersistence();
                })
            .Build();
        var scopeFactory = _host.Services.GetRequiredService<IServiceScopeFactory>();
        _scope = scopeFactory.CreateScope();
    }

    public static App Create() => new();

    internal T GetRequiredService<T>()
    {
        if (_scope.ServiceProvider.GetService<T>() is not { } service)
        {
            throw new InvalidOperationException($"Service {typeof(T)} needs to be configured");
        }
        return service;
    }

    internal void Dispose()
    {
        _host.Dispose();
    }
}
