using Infrastructure;
using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;

namespace Application.Test.Common.Host;

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
                    services.AddInfrastructure("application-test");
                    services.AddPersistence();
                    services.AddApplication();
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

    internal void Dispose()
    {
        _host.Dispose();
    }
}
