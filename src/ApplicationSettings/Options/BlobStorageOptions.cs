namespace ApplicationSettings.Options;
public class BlobStorageOptions : IApplicationOptions
{
    [Required]
    public string ConnectionString { get; set; } = default!;
    public int MaximumRetryCount { get; set; } = 3;
}
