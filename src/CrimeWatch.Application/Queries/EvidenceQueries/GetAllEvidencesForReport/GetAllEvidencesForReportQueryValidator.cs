using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Queries.EvidenceQueries.GetAllEvidencesForReport;
public sealed class GetAllEvidencesForReportQueryValidator : HttpContextValidator<GetAllEvidencesForReportQuery>
{
    public GetAllEvidencesForReportQueryValidator(
        IAuthenticationService authenticationService,
        IOptions<AppOptions> appOptions) : base(authenticationService)
    {
        if (!appOptions.Value.ModeratedContent) return;
        RuleFor(e => e)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to perform this action.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(GetAllEvidencesForReportQuery query)
        => _authenticationService.Authenticate().IsModerator;
}
