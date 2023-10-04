using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.ReportQueries.GetReport;
public class GetReportQueryValidator : HttpContextValidator<GetReportQuery>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetReportQueryValidator(IHttpContextAccessor httpContextAccessor,
        IRepository<Report, ReportId> reportRepository,
        IOptions<AppOptions> appOptions) : base(httpContextAccessor)
    {
        _reportRepository = reportRepository;
        if (!appOptions.Value.ModeratedContent) return;
        RuleFor(e => e.ReportId)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to view this report.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(ReportId reportId, CancellationToken cancellationToken)
    {
        var report =
            await _reportRepository.GetByIdAsync(reportId, e => new { e.Status, e.WitnessId }, cancellationToken);

        if (report is null) return false;
        if (UserClaims.UserType.Equals(UserType.Moderator)) return true;
        return !report.Status.Equals(Status.Pending) || report.WitnessId.Equals(UserClaims.WitnessId);
    }
}
