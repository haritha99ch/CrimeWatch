using CrimeWatch.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace CrimeWatch.Web.API.OptionConfigurations;
public class JwtOptionsConfiguration : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public JwtOptionsConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        // Generate a random string as secret key for testing.
        var secretKey = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(secretKey);
        options.Secret = Convert.ToBase64String(secretKey);

    }
}
