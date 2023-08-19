using CrimeWatch.AppSettings.Helpers;
using Microsoft.Extensions.Configuration;

namespace CrimeWatch.AppSettings;
public static class Configure
{
    public static void AddAppSettings(this IConfigurationBuilder configuration)
        => configuration.AddUserSecrets<AssemblyReference>().Validate();
}
