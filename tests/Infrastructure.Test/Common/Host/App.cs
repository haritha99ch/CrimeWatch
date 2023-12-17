using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Test.Common.Host;
public class App
{
    private readonly IServiceScope _scope;
    public ApplicationDbContext DbContext => _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    private App()
    {
        var host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .ConfigureServices((_, services) => services.AddInfrastructure("test-crime-watch-db"))
            .Build();

        var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
        _scope = scopeFactory.CreateScope();
    }

    public static App Create() => new();
}
