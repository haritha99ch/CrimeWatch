using CrimeWatch.Application.Options;
using Microsoft.Extensions.Options;

namespace CrimeWatch.Web.API.OptionConfigurations;
public class JwtOptionsConfiguration : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    private const string JwtSecretSectionName = "Jwt";

    public JwtOptionsConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(JwtSecretSectionName).Bind(options);
    }
}
