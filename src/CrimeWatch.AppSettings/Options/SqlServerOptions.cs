namespace CrimeWatch.AppSettings.Options;
public class SqlServerOptions
{
    [Required]
    public string ConnectionString { get; set; } = default!;
    [Required]
    public int MaxRetryCount { get; set; }
    [Required]
    public int CommandTimeout { get; set; }
    [Required]
    public bool EnableDetailedErrors { get; set; }
    [Required]
    public bool EnableSensitiveDataLogging { get; set; }
}
