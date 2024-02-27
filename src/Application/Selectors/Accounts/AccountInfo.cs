namespace Application.Selectors.Accounts;
public record AccountInfo(string FullName, string Email, string PhoneNumber, bool IsModerator) : ISelector;
