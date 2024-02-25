using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.DeleteReport;
internal sealed class DeleteReportCommandValidator : ApplicationValidator<DeleteReportCommand>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IAuthenticationService _authenticationService;
    public DeleteReportCommandValidator(
            IRepository<Report, ReportId> reportRepository,
            IAuthenticationService authenticationService
        )
    {
        _reportRepository = reportRepository;
        _authenticationService = authenticationService;
        RuleFor(e => e.ReportId).MustAsync(IsAuthorizedAsync).WithState(_ => validationError);
    }
    private async Task<bool> IsAuthorizedAsync(ReportId reportId, CancellationToken cancellationToken)
    {
        AccountId currentAccountId = new(Guid.Empty);
        var authenticationResult = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        var isAuthenticated = authenticationResult.Handle(
            e =>
            {
                currentAccountId = e.AccountId;
                return true;
            },
            e =>
            {
                validationError = e;
                return false;
            });
        if (!isAuthenticated) return false;

        var report = await _reportRepository.GetOneAsync<ReportAuthorizationInfoById, ReportAuthorizationInfo>(
            new(reportId),
            cancellationToken);

        if (report is null)
        {
            validationError = ReportNotFoundError.Create(message: "Report is not found to delete.");
            return false;
        }

        if (report.AuthorId != null && report.AuthorId.Equals(currentAccountId)) return true;

        validationError = UnauthorizedError.Create(message: "You are not authorize to delete report.");
        return false;
    }
}
