namespace CrimeWatch.AppSettings.Options;
public class AppOptions
{
    [Required]
    public bool Validations { get; set; }
    [Required]
    public bool ModeratedContent { get; set; }
}
