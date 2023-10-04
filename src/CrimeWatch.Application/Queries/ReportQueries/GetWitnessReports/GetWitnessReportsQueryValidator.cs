namespace CrimeWatch.Application.Queries.ReportQueries.GetWitnessReports;
public class GetWitnessReportsQueryValidator : HttpContextValidator<GetWitnessReportsQuery>
{

    public GetWitnessReportsQueryValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(e => e.WitnessId)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to view this witness's reports.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(WitnessId witnessId)
        => UserClaims.WitnessId is not null && UserClaims.WitnessId.Equals(UserClaims.WitnessId);
}
