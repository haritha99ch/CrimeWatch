namespace CrimeWatch.AppSettings.Options;
public class SqlServerOptions
{
    [Required]
    public string ConnectionString { get; set; } = default!;
}
