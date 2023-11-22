using Infrastructure;
using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Persistence.Test.Common.Host;

public class App
{
    private readonly IHost _host;
    public ApplicationDbContext DbContext =>
        _host.Services.GetRequiredService<ApplicationDbContext>();

    private App()
    {
        _host = Microsoft
            .Extensions
            .Hosting
            .Host
            .CreateDefaultBuilder()
            .ConfigureServices(
                (_, services) =>
                {
                    services.AddInfrastructure("persistence-test");
                    services.AddPersistence();
                }
            )
            .Build();
    }

    public static App Create() => new();

    internal T GetRequiredService<T>()
    {
        if (_host.Services.GetService<T>() is not T service)
        {
            throw new InvalidOperationException($"Service {typeof(T)} needs to be configured");
        }
        return service;
    }
}
