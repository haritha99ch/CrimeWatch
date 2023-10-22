using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Queries.ReportQueries.GetAllReports;
public class GetAllReportsQueryValidator : HttpContextValidator<GetAllReportsQuery>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetAllReportsQueryValidator(
        IAuthenticationService authenticationService,
        IOptions<AppOptions> appOptions, IRepository<Report, ReportId> reportRepository) : base(authenticationService)
    {
        _reportRepository = reportRepository;
        if (!appOptions.Value.ModeratedContent) return;
        RuleFor(e => e)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to process the request")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(GetAllReportsQuery query) => _authenticationService.Authenticate().IsModerator;
}
