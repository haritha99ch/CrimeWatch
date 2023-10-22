using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Queries.ReportQueries.GetModeratorReports;
public class GetModeratorReportsQueryValidator : HttpContextValidator<GetModeratorReportsQuery>
{

    public GetModeratorReportsQueryValidator(IAuthenticationService authenticationService) : base(authenticationService)
    {
        RuleFor(e => e.ModeratorId)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to perform this action.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(ModeratorId moderatorId)
        => _authenticationService.Authenticate()
            .Authorize(moderatorId: moderatorId.Equals);
}
