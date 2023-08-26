using Microsoft.Extensions.Hosting;

namespace CrimeWatch.Infrastructure.Test.Primitives;
[TestClass]
public abstract class RepositoryTests
{
    private IHost _host { get; }

    protected RepositoryTests(string databaseName = "TestDatabase")
    {
        _host = Host
           .CreateDefaultBuilder()
           .ConfigureServices(service => service.AddInfrastructure(
               string.Empty,
               options => options.UseInMemoryDatabase(databaseName)))
           .Build();
    }

    protected T GetService<T>() where T : class
    {
        if (_host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in AddInfrastructure within Configure.cs.");
        }
        return service;
    }
}
