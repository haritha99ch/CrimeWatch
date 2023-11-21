using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Test.Common.Host;

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
            .ConfigureServices((_, services) => services.AddInfrastructure("test-crime-watch-db"))
            .Build();
    }

    public static App Create() => new();
}
