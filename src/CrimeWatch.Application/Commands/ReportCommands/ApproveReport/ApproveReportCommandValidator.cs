using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.ReportCommands.ApproveReport;
public class ApproveReportCommandValidator : HttpContextValidator<ApproveReportCommand>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public ApproveReportCommandValidator(
        IAuthenticationService authenticationService,
        IRepository<Report, ReportId> reportRepository) : base(authenticationService)
    {
        _reportRepository = reportRepository;
        RuleFor(e => e)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to moderate this report.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(ApproveReportCommand command, CancellationToken cancellationToken)
    {
        var result = _authenticationService.Authenticate();
        return await result.Authorize<Task<bool>>(
            async moderatorId => await _reportRepository.HasPermissionsToModerateAsync(command.ReportId,
                moderatorId, cancellationToken),
            Task.FromResult(false));
    }
}
