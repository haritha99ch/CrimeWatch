using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ReportCommands.UpdateReport;
public class UpdateReportCommandValidator : HttpContextValidator<UpdateReportCommand>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public UpdateReportCommandValidator(
        IHttpContextAccessor httpContextAccessor,
        IRepository<Report, ReportId> reportRepository) : base(httpContextAccessor)
    {
        _reportRepository = reportRepository;
        RuleFor(e => e)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to update this report.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(UpdateReportCommand command, CancellationToken cancellationToken)
        => UserClaims.WitnessId != null
            && await _reportRepository.HasPermissionsToEditAsync(command.Id, UserClaims.WitnessId, cancellationToken);
}
