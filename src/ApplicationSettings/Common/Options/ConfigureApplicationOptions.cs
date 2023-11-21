using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ApplicationSettings.Common.Options;

public class ConfigureApplicationOptions<T> : IConfigureOptions<T>, IPostConfigureOptions<T>
    where T : class
{
    private readonly IConfiguration _configuration;

    public ConfigureApplicationOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(T options) => _configuration.GetSection(typeof(T).Name).Bind(options);

    public void PostConfigure(string? name, T options)
    {
        try
        {
            Validator.ValidateObject(options, new(options), true);
        }
        catch (Exception e)
        {
            throw new(
                $"\nCheck the following properties of section {typeof(T).Name}, section in user secrets or appsettings.json:\n{e.Message}"
            );
        }
    }
}
