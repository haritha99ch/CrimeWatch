namespace ApplicationSettings.Options;

public class BlobStorageOptions : IApplicationOptions
{
    [Required]
    public string ConnectionString { get; set; } = default!;
}
