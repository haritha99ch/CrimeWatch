namespace Shared.Models.Accounts;
public record WitnessDetailsForReportDetails(
        AccountId AccountId,
        string FullName,
        string Email,
        string PhoneNumber
    ) : ISelector;
