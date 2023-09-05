using Microsoft.Extensions.Configuration;

namespace CrimeWatch.AppSettings.Helpers;
internal static class AppSettingsValidator
{
    public static void Validate(this IConfigurationBuilder config)
    {
        IConfigurationRoot? _config = config.Build();
        List<string> errors = new();

        if (_config.GetConnectionString("Database:DefaultConnection") == null)
            errors.Add("Missing ConnectionStrings:Database:DefaultConnection");

        if (_config.GetConnectionString("Storage:DefaultConnection") == null)
            errors.Add("Missing ConnectionStrings:Storage:DefaultConnection");

        if (errors.Count == 0) return;
        string message = $"Missing User Secrets. Use 'dotnet user-secrets set' to configure user secrets.\n";
        message = $"{message}{string.Join("\n", errors)}";
        throw new Exception(message);
    }
}