namespace Application.Selectors.Reports;
public sealed record ReportAuthorizationInfo(AccountId? AuthorId) : Selector<Report, ReportAuthorizationInfo>
{
    protected override Expression<Func<Report, ReportAuthorizationInfo>> Select() => e => new(e.AuthorId);
}
