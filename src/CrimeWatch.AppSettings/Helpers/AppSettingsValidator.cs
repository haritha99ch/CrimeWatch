using Microsoft.Extensions.Configuration;

namespace CrimeWatch.AppSettings.Helpers;
internal static class AppSettingsValidator
{
    public static void Validate(this IConfigurationBuilder config)
    {
        IConfigurationRoot? _config = config.Build();
        List<string> errors = new();

        if (_config.GetConnectionString("DefaultConnection") == null)
            errors.Add("Missing ConnectionStrings:DefaultConnection");

        if (errors.Count == 0) return;
        string message = $"Missing User Secrets. Use 'dotnet user-secrets set' to configure user secrets.\n";
        message = $"{message}{string.Join("\n", errors)}";
        throw new Exception(message);
    }
}