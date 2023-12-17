using Application.Behaviors;
using Application.Contracts.Services;
using Application.Services;
using ApplicationSettings;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class Configure
{
    /// <summary>
    ///     Add Application Services to the specified
    ///     <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
    ///     <para>
    ///         Required Dependencies:
    ///         <para>
    ///             <see
    ///                 cref="ApplicationSettings.Configure.AddApplicationSettings(Microsoft.Extensions.Configuration.IConfigurationBuilder)" />
    ///             ,
    ///         </para>
    ///         <para><see cref="Infrastructure.Configure.AddInfrastructure(IServiceCollection)" />,</para>
    ///         <para><see cref="Persistence.Configure.AddPersistence(IServiceCollection)" />,</para>
    ///         <para>
    ///             <see cref="Microsoft.AspNetCore.Http.HttpContextAccessor" />
    ///         </para>
    ///         <see cref="" />
    ///     </para>
    /// </summary>
    /// <param name="services"></param>
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    public static void AddApplication(this IServiceCollection services)
    {
        services.ConfigureJwtOptions();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AssemblyReference>())
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    public static void AddApplicationValidators(this IServiceCollection services) =>
        services.AddValidatorsFromAssemblyContaining<AssemblyReference>(includeInternalTypes: true);
}
