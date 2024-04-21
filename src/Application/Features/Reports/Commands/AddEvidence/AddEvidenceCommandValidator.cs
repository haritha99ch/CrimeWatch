using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.AddEvidence;
internal sealed class AddEvidenceCommandValidator : ApplicationValidator<AddEvidenceCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public AddEvidenceCommandValidator(
            IAuthenticationService authenticationService,
            IRepository<Report, ReportId> reportRepository
        )
    {
        _authenticationService = authenticationService;
        _reportRepository = reportRepository;

        RuleFor(e => e).MustAsync(IsAuthorizedAsync).WithState(_ => validationError);
    }
    private async Task<bool> IsAuthorizedAsync(AddEvidenceCommand command, CancellationToken cancellationToken)
    {
        var authenticationResult = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        var isAuthenticated = authenticationResult.Handle(
            e =>
            {
                if (e.AccountId.Equals(command.AuthorId)) return true;
                validationError = UnauthorizedError.Create(message: "Mismatching current user and evidence author");
                return false;
            },
            e =>
            {
                validationError = e;
                return false;
            }
        );
        if (!isAuthenticated) return false;

        var report = await _reportRepository.GetOneAsync<ReportAuthorizationInfoById, ReportAuthorizationInfo>(
            new(command.ReportId),
            cancellationToken);

        if (report is not null)
        {
            if (report.Status.Equals(Status.Approved)) return true;
            validationError = UnauthorizedError.Create("Unauthorized to update",
                "Report is not approved to update.");
            return false;
        }
        validationError = ReportNotFoundError.Create(message: "Report not found to add evidence");
        return false;
    }

}
