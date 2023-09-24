namespace CrimeWatch.Application.Permissions.Commands.ReportCommands.EditReport;
internal class EditReportPermissionsHandler : RequestPermissions, IRequestHandler<EditReportPermissions, ReportPermissions>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public EditReportPermissionsHandler(
        IHttpContextAccessor httpContextAccessor,
        IRepository<Report, ReportId> reportRepository) : base(httpContextAccessor)
    {
        _reportRepository = reportRepository;
    }

    public async Task<ReportPermissions> Handle(EditReportPermissions request, CancellationToken cancellationToken)
    {
        return _userClaims.UserType switch
        {
            UserType.Moderator => await GetModeratorPermissions(request, cancellationToken),
            UserType.Witness => await GetWitnessPermissions(request, cancellationToken),
            _ => ReportPermissions.Denied
        };
    }

    private async Task<ReportPermissions> GetWitnessPermissions(EditReportPermissions request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.GetByIdAsync(request.ReportId, e => new { e.WitnessId }, cancellationToken);
        if (report == null) return ReportPermissions.Denied;
        return report.WitnessId.Equals(_userClaims.WitnessId) ? ReportPermissions.Granted : ReportPermissions.Denied;
    }

    private async Task<ReportPermissions> GetModeratorPermissions(EditReportPermissions request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.GetByIdAsync(request.ReportId, e => new { e.ModeratorId }, cancellationToken);
        if (report == null) return ReportPermissions.Denied;
        if (report.ModeratorId == null) return ReportPermissions.Granted;
        return report.ModeratorId.Equals(_userClaims.ModeratorId) ? ReportPermissions.Granted : ReportPermissions.Denied;
        throw new NotImplementedException();
    }
}
