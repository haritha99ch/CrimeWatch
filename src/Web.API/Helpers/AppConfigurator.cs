using Application;
using ApplicationSettings;
using ApplicationSettings.Helpers;
using ApplicationSettings.Options;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.Text;
using Web.API.Middlewares;

namespace Web.API.Helpers;
internal static class AppConfigurator
{
    public static void ConfigureConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddApplicationSettings();
    }

    public static void ConfigureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddInfrastructure();
        serviceCollection.AddPersistence();
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddApplication();
        serviceCollection.AddApplicationValidators();
        serviceCollection.AddTransient<ErrorHandlingMiddleWare>();
    }

    public static void AddJwtAuthentication(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureJwtOptions();
        var jwtOptions = serviceCollection.GetRequiredOptions<JwtOptions>();
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Audience = jwtOptions.Audience;
                options.Authority = jwtOptions.Issuer;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
                };
            });
    }
}
