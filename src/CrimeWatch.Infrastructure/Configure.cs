using CrimeWatch.Infrastructure.Contexts;
using CrimeWatch.Infrastructure.Contracts.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrimeWatch.Infrastructure;
public static class Configure
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
            options => options.UseSqlServer(connectionString));
    }
}
