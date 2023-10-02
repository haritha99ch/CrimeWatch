using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Permissions.Queries.ReportQueries.ViewReport;
internal class ViewReportPermissionsHandler : RequestPermissions,
    IRequestHandler<GetReportPermissions, ReportPermissions>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    public ViewReportPermissionsHandler(
        IHttpContextAccessor httpContextAccessor,
        IRepository<Report, ReportId> reportRepository) : base(httpContextAccessor)
    {
        _reportRepository = reportRepository;
    }

    public async Task<ReportPermissions> Handle(GetReportPermissions request, CancellationToken cancellationToken)
    {
        if (request.ReportId != null) return await GetPermissionsToViewReportById(request.ReportId, cancellationToken);
        return UserClaims.UserType switch
        {
            UserType.Moderator => ReportPermissions.FullAccess,
            _ => ReportPermissions.Moderated
        };
    }

    private async Task<ReportPermissions> GetPermissionsToViewReportById(ReportId reportId,
        CancellationToken cancellationToken)
    {
        var report =
            await _reportRepository.GetByIdAsync(reportId, e => new { e.Status, e.WitnessId }, cancellationToken);
        if (report == null) return ReportPermissions.Denied;
        return UserClaims.UserType switch
        {
            UserType.Witness =>
                report.WitnessId.Equals(UserClaims.WitnessId) ?
                    ReportPermissions.Granted :
                    report.Status.Equals(Status.UnderReview) || report.Status.Equals(Status.Approved) ?
                        ReportPermissions.Granted : ReportPermissions.Denied,
            UserType.Moderator => ReportPermissions.Granted,
            _ => ReportPermissions.Denied
        };
    }
}
