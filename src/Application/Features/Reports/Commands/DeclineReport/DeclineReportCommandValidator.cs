using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.DeclineReport;
internal sealed class DeclineReportCommandValidator : ApplicationValidator<DeclineReportCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public DeclineReportCommandValidator(
            IAuthenticationService authenticationService,
            IRepository<Report, ReportId> reportRepository
        )
    {
        _authenticationService = authenticationService;
        _reportRepository = reportRepository;

        RuleFor(e => e.ReportId)
            .MustAsync(IsAuthorizedAsync)
            .WithState(_ => validationError);
    }
    private async Task<bool> IsAuthorizedAsync(ReportId reportId, CancellationToken cancellationToken)
    {
        var authResult = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        var currentUser = new AccountId(new());
        var isModerator = false;
        var isAuthenticated = authResult.Handle(
            e =>
            {
                currentUser = e.AccountId;
                isModerator = e.IsModerator;
                return true;
            },
            e =>
            {
                validationError = e;
                return false;
            });
        if (!isAuthenticated) return false;
        if (!isModerator)
        {
            validationError = UnauthorizedError.Create(message: "Only moderators can decline reports.");
            return false;
        }
        var report = await _reportRepository.GetOneAsync<ReportAuthorizationInfoById, ReportAuthorizationInfo>(
            new(reportId),
            cancellationToken);
        if (report is null)
        {
            validationError = ReportNotFoundError.Create(message: "Report is not found to decline.");
            return false;
        }

        if (!report.Status.Equals(Status.Approved) && !report.Status.Equals(Status.UnderReview))
        {
            validationError = ReportIsNoUnderReviewError.Create(message: "Report must be under reviewed to declined.");
            return false;
        }

        if (report.ModeratorId == null || report.ModeratorId.Equals(currentUser)) return true;
        validationError = UnauthorizedError.Create(message: "Only moderator reviewing the report can decline.");
        return false;
    }
}
