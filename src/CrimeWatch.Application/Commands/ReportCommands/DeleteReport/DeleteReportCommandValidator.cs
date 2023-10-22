using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.ReportCommands.DeleteReport;
public class DeleteReportCommandValidator : HttpContextValidator<DeleteReportCommand>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public DeleteReportCommandValidator(
        IAuthenticationService authenticationService,
        IRepository<Report, ReportId> reportRepository) : base(authenticationService)
    {
        _reportRepository = reportRepository;
        RuleFor(e => e)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to update this report.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(DeleteReportCommand command, CancellationToken cancellationToken)
    {
        var result = _authenticationService.Authenticate();
        return await result.Authorize<Task<bool>>(
            async witnessId => await _reportRepository.HasPermissionsToEditAsync(command.Id, witnessId,
                cancellationToken),
            Task.FromResult(false));
    }
}
