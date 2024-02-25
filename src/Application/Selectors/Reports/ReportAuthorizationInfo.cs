namespace Application.Selectors.Reports;
public sealed record ReportAuthorizationInfo(AccountId? AuthorId, AccountId? ModeratorId, Status Status) : ISelector;
