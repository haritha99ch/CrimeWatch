namespace CrimeWatch.Application.Queries.ReportQueries.GetModeratorReports;
public class GetModeratorReportsQueryValidator : HttpContextValidator<GetModeratorReportsQuery>
{

    public GetModeratorReportsQueryValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(e => e.ModeratorId)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to perform this action.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(ModeratorId moderatorId)
        => UserClaims.ModeratorId is not null && moderatorId.Equals(UserClaims.ModeratorId);
}
