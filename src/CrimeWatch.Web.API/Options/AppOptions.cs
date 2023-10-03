using System.ComponentModel.DataAnnotations;

namespace CrimeWatch.Web.API.Options;
public class AppOptions
{
    [Required]
    public bool Validations { get; set; }
}
