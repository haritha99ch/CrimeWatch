using CrimeWatch.Application.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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
        options.Secret = "7H15_15_4_R4ND0M_53CR37_K3Y_F0R_T35T1N9";
    }
}
