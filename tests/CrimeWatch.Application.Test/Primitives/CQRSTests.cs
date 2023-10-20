using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Application.Test.OptionConfigurations;
using CrimeWatch.Application.Test.Services;
using CrimeWatch.Infrastructure.Contracts.Contexts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CrimeWatch.Application.Test.Primitives;
[TestClass]
public abstract class CQRSTests
{

    protected readonly IApplicationDbContext _dbContext;
    protected readonly IMediator _mediator;

    protected CQRSTests(string databaseName)
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(
                service =>
                {
                    service.AddApplication(options =>
                    {
                        options.UseInMemoryDatabase(databaseName);
                        options.EnableSensitiveDataLogging();
                    });
                    service.AddTransient<IFileStorageService, InMemoryBlobStorageService>();
                    service.ConfigureOptions<JwtOptionsConfiguration>();
                })
            .Build();

        _dbContext = GetService<IApplicationDbContext>();
        _mediator = GetService<IMediator>();
    }

    private IHost _host { get; }

    private T GetService<T>() where T : class
    {
        if (_host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException(
                $"{typeof(T)} needs to be registered in AddInfrastructure within Configure.cs.");
        }
        return service;
    }

    [ClassCleanup]
    public void ClassCleanUp() => _host.Dispose();
}
