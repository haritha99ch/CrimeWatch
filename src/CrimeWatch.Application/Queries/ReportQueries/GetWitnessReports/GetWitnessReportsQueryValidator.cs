using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Queries.ReportQueries.GetWitnessReports;
public class GetWitnessReportsQueryValidator : HttpContextValidator<GetWitnessReportsQuery>
{

    public GetWitnessReportsQueryValidator(IAuthenticationService authenticationService) : base(authenticationService)
    {
        RuleFor(e => e.WitnessId)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to view this witness's reports.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(WitnessId witnessId)
        => _authenticationService.Authenticate().Authorize(witnessId: witnessId.Equals);
}
