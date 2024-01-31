using Application.Common.Validators;
using FluentValidation;

namespace Application.Features.Reports.Queries.GetReportEvidencesById;
public sealed class GetReportEvidencesByIdQueryValidator : ApplicationValidator<GetReportEvidencesByIdQuery>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetReportEvidencesByIdQueryValidator(
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
    private async Task<bool> IsAuthorizedAsync(ReportId reportId, CancellationToken cancellation)
    {
        var authenticationResult = await _authenticationService.GetAuthenticationResultAsync(cancellation);
        AccountId? currentAccountId = default;
        var isModerator = false;

        authenticationResult.Handle(e =>
        {
            currentAccountId = e.AccountId;
            isModerator = e.IsModerator;
        });

        if (isModerator) return true;

        var report =
            await _reportRepository.GetByIdAsync(reportId, ReportAuthorizationInfo.SelectQueryable(), cancellation);
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
