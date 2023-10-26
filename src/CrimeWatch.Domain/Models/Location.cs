namespace CrimeWatch.Domain.Models;
public class Location
{
    public string? No { get; set; } = string.Empty;
    public string Street1 { get; set; } = string.Empty;
    public string? Street2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;

    public static Location Create(string? no, string street1, string? street2, string city, string province)
    {
        return new()
        {
            No = no,
            Street1 = street1,
            Street2 = street2,
            City = city,
            Province = province
        };
    }
}
