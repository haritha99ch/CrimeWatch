namespace CrimeWatch.Application.Queries.ReportQueries.GetAllReports;
public class GetAllReportsQueryValidator : HttpContextValidator<GetAllReportsQuery>
{

    public GetAllReportsQueryValidator(
        IHttpContextAccessor httpContextAccessor,
        IOptions<AppOptions> appOptions) : base(httpContextAccessor)
    {
        if (!appOptions.Value.ModeratedContent) return;
        RuleFor(e => e)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to process the request")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(GetAllReportsQuery query) => UserClaims.UserType.Equals(UserType.Moderator);
}
