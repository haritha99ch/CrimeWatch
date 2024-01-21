namespace Shared.Dto.Accounts;
public record ModeratorDetailsForReportDetails(
        AccountId ModeratorId,
        string FullName,
        string Email,
        string PhoneNumber,
        string City,
        string Province
    );
