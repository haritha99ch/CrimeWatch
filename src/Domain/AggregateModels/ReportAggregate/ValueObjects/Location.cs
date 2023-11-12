namespace Domain.AggregateModels.ReportAggregate.ValueObjects;
public sealed record Location
{
    public string? No { get; private set; }
    public required string Street1 { get; set; }
    public string? Street2 { get; private set; }
    public required string City { get; set; }
    public required string Province { get; set; }

    public static Location Create(string? no, string street1, string? street2, string city, string province) => new()
    {
        No = no,
        Street1 = street1,
        Street2 = street2,
        City = city,
        Province = province
    };

    public bool Update(string? no, string street1, string? street2, string city, string province)
    {
        if (No == no && Street1 == street1 && Street2 == street2 && City == city && Province == province) return false;

        No = no;
        Street1 = street1;
        Street2 = street2;
        City = city;
        Province = province;

        return true;
    }
}
