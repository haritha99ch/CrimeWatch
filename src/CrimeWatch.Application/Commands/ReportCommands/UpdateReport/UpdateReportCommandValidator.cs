using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.ReportCommands.UpdateReport;
public class UpdateReportCommandValidator : HttpContextValidator<UpdateReportCommand>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public UpdateReportCommandValidator(
        IAuthenticationService authenticationService,
        IRepository<Report, ReportId> reportRepository) : base(authenticationService)
    {
        _reportRepository = reportRepository;
        RuleFor(e => e)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to update this report.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(UpdateReportCommand command, CancellationToken cancellationToken)
    {
        var result = _authenticationService.Authenticate();
        return await result.Authorize<Task<bool>>(
            async witnessId => await _reportRepository.HasPermissionsToEditAsync(command.Id, witnessId,
                cancellationToken),
            Task.FromResult(false));
    }
}
