using ApplicationSettings;
using ApplicationSettings.Options;
using Infrastructure;
using Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using Persistence;

namespace Application.Test.Common.Host;
public class App
{
    private readonly IHost _host;
    private readonly IServiceScope _scope;
    public ApplicationDbContext DbContext => GetRequiredService<ApplicationDbContext>();

    private App()
    {
        _host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .ConfigureHostConfiguration(config => config.AddApplicationSettings())
            .ConfigureServices(
                (_, services) =>
                {
                    services.AddInfrastructure("application-test");
                    services.AddPersistence();

                    // Mock HttpContextAccessor
                    var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
                    var context = new DefaultHttpContext();
                    mockHttpContextAccessor.Setup(e => e.HttpContext).Returns(context);
                    services.AddSingleton(mockHttpContextAccessor.Object);

                    // Mock JwtOptions
                    var jwtOptions = new JwtOptions
                    {
                        Issuer = "TestIssuer",
                        Audience = "TestAudience",
                        Secret = DataProvider.TestSecretKey
                    };
                    services.AddSingleton<IOptions<JwtOptions>>(new OptionsWrapper<JwtOptions>(jwtOptions));

                    services.AddApplication();
                    services.AddApplicationValidators();
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
