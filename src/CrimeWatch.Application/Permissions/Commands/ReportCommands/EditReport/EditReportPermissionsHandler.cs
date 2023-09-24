namespace CrimeWatch.Application.Permissions.Commands.ReportCommands.EditReport;
internal class EditReportPermissionsHandler : IRequestHandler<EditReportPermissions, ReportPermissions>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    private HttpContext _httpContext => _contextAccessor.HttpContext ?? throw new Exception("Not an API");
    private UserClaims _userClaims = default!;

    public EditReportPermissionsHandler(IRepository<Report, ReportId> reportRepository, IHttpContextAccessor contextAccessor)
    {
        _reportRepository = reportRepository;
        _contextAccessor = contextAccessor;
    }

    public async Task<ReportPermissions> Handle(EditReportPermissions request, CancellationToken cancellationToken)
    {
        _userClaims = _httpContext.GetUserClaims();
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
