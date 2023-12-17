namespace ApplicationSettings.Options;
public class JwtOptions : IApplicationOptions
{
    [Required]
    public string Secret { get; set; } = default!;

    [Required]
    public string Issuer { get; set; } = default!;

    [Required]
    public string Audience { get; set; } = default!;
}
