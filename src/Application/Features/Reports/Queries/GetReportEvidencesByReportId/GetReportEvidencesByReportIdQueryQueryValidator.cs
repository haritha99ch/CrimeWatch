using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Queries.GetReportEvidencesByReportId;
public sealed class
    GetReportEvidencesByReportIdQueryQueryValidator : ApplicationValidator<GetReportEvidencesByReportIdQuery>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetReportEvidencesByReportIdQueryQueryValidator(
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
        var authenticationResult = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        AccountId? currentAccountId = default;
        var isModerator = false;

        authenticationResult.Handle(e =>
        {
            currentAccountId = e.AccountId;
            isModerator = e.IsModerator;
        });

        if (isModerator) return true;

        var report = await _reportRepository.GetOneAsync<ReportAuthorizationInfoById, ReportAuthorizationInfo>(
            new(reportId),
            cancellationToken);

        if (report is null)
        {
            validationError = ReportNotFoundError.Create();
            return false;
        }
        if (report.Status.Equals(Status.Approved)) return true;
        if (report.AuthorId != null && report.AuthorId.Equals(currentAccountId)) return true;
        validationError = UnauthorizedError.Create(message: "You are not authorized to view report evidences.");
        return false;
    }

}
