using Microsoft.Extensions.DependencyInjection;

namespace CrimeWatch.Application;
public static class Configure
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AssemblyReference>());
    }
}
