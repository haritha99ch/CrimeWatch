namespace ApplicationSettings.Options;
public class JwtOptions : IApplicationOptions
{
    [Required]
    public string Secret { get; init; } = default!;

    [Required]
    public string Issuer { get; init; } = default!;

    [Required]
    public string Audience { get; init; } = default!;
}
