using Application.Common.Validators;
using FluentValidation;

namespace Application.Features.Reports.Queries.GetReportDetailsById;
public sealed class GetReportDetailsByIdQueryValidator : ApplicationValidator<GetReportDetailsByIdQuery>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetReportDetailsByIdQueryValidator(
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
        var result = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        AccountId? currentAccountId = default;
        var isModerator = false;

        result.Handle(
            e =>
            {
                currentAccountId = e.AccountId;
                isModerator = e.IsModerator;
            });

        if (isModerator) return true;

        var report = await _reportRepository
            .GetByIdAsync(reportId, ReportAuthorizationInfo.GetSelector, cancellationToken);

        if (report is null)
        {
            validationError = ReportNotFoundError.Create(message: "Report not found!.");
            return false;
        }
        if (report.Status.Equals(Status.Approved)) return true;
        if (report.AuthorId!.Equals(currentAccountId)) return true;
        validationError = UnauthorizedError.Create(message: "You are not authorized to view this report");
        return false;
    }

}
