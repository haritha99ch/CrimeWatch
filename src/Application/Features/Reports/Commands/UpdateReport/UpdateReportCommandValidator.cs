using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.UpdateReport;
internal sealed class UpdateReportCommandValidator : ApplicationValidator<UpdateReportCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;
    private AccountId _currentUserId = new(Guid.Empty);

    public UpdateReportCommandValidator(
            IAuthenticationService authenticationService,
            IRepository<Report, ReportId> reportRepository
        )
    {
        _authenticationService = authenticationService;
        _reportRepository = reportRepository;
        RuleFor(e => e.ReportId).MustAsync(Authorized).WithState(_ => validationError);
    }

    private async Task<bool> Authorized(ReportId reportId, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);

        var isAuthenticated = result.Handle(
            e =>
            {
                _currentUserId = e.AccountId;
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
            validationError = ReportNotFoundError.Create(message: "Report is not found to update.");
            return false;
        }

        if (report.AuthorId != null && report.AuthorId.Equals(_currentUserId)) return true;

        validationError = UnauthorizedError.Create(message: "You are not authorize to update report.");
        return false;
    }
}
