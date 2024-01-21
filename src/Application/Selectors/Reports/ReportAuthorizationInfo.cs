﻿namespace Application.Selectors.Reports;
public sealed record ReportAuthorizationInfo(AccountId? AuthorId, AccountId? ModeratorId, Status Status)
    : Selector<Report, ReportAuthorizationInfo>
{
    protected override Expression<Func<Report, ReportAuthorizationInfo>> Select()
        => e => new(e.AuthorId, e.ModeratorId, e.Status);
}
