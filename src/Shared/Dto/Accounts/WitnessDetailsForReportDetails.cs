namespace Shared.Dto.Accounts;
public record WitnessDetailsForReportDetails(
        AccountId AccountId,
        string FullName,
        string Email,
        string PhoneNumber
    );
