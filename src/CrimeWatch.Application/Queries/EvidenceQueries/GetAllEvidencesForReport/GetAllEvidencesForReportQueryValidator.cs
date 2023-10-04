namespace CrimeWatch.Application.Queries.EvidenceQueries.GetAllEvidencesForReport;
public sealed class GetAllEvidencesForReportQueryValidator : HttpContextValidator<GetAllEvidencesForReportQuery>
{
    public GetAllEvidencesForReportQueryValidator(
        IHttpContextAccessor httpContextAccessor,
        IOptions<AppOptions> appOptions) : base(httpContextAccessor)
    {
        if (!appOptions.Value.ModeratedContent) return;
        RuleFor(e => e)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to perform this action.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(GetAllEvidencesForReportQuery query) => UserClaims.UserType.Equals(UserType.Moderator);
}
