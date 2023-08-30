using CrimeWatch.Infrastructure.Contracts.Contexts;
using MediatR;
using Microsoft.Extensions.Hosting;

namespace CrimeWatch.Application.Test.Primitives;
[TestClass]
public abstract class CQRSTests
{
    private IHost _host { get; }

    protected readonly IApplicationDbContext _dbContext;
    protected readonly IMediator _mediator;

    protected CQRSTests(string databaseName)
    {
        _host = Host
           .CreateDefaultBuilder()
           .ConfigureServices(service => service.AddApplication(
               options => options.UseInMemoryDatabase(databaseName)))
           .Build();

        _dbContext = GetService<IApplicationDbContext>();
        _mediator = GetService<IMediator>();
    }

    protected T GetService<T>() where T : class
    {
        if (_host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in AddInfrastructure within Configure.cs.");
        }
        return service;
    }

    [ClassCleanup]
    public void ClassCleanUp() => _host.Dispose();
}
