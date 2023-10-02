using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace CrimeWatch.Web.API.Primitives;
public class ConfigureOptions<T> : IConfigureOptions<T>, IPostConfigureOptions<T> where T : class
{
    private readonly IConfiguration _configuration;

    public ConfigureOptions(IConfiguration configuration)
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
                $"\nCheck the following properties of section {typeof(T).Name}, section in user appsettings.json:\n{e.Message}");
        }
    }
}
