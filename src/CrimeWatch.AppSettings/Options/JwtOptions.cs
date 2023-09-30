namespace CrimeWatch.AppSettings.Options;
public class JwtOptions
{
    [Required]
    public string Secret { get; set; } = default!;
    [Required]
    public string Issuer { get; set; } = default!;
    [Required]
    public string Audience { get; set; } = default!;
}
