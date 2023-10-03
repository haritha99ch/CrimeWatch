using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ReportCommands.DeclineReport;
public class DeclineReportCommandValidator : HttpContextValidator<DeclineReportCommand>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public DeclineReportCommandValidator(
        IHttpContextAccessor httpContextAccessor,
        IRepository<Report, ReportId> reportRepository) : base(httpContextAccessor)
    {
        _reportRepository = reportRepository;
        RuleFor(e => e)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to moderate this report.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(DeclineReportCommand command, CancellationToken cancellationToken)
    {
        if (!UserClaims.UserType.Equals(UserType.Moderator)) return false;
        return
            await _reportRepository.HasPermissionsToModerate(command.ReportId, UserClaims.ModeratorId,
                cancellationToken);

    }
}
