namespace CrimeWatch.AppSettings.Options;
public class BlobStorageOptions
{
    [Required]
    public string ConnectionString { get; set; } = default!;
}
