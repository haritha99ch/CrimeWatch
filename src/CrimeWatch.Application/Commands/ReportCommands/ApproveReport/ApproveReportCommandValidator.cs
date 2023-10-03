using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ReportCommands.ApproveReport;
public class ApproveReportCommandValidator : HttpContextValidator<ApproveReportCommand>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public ApproveReportCommandValidator(
        IHttpContextAccessor httpContextAccessor,
        IRepository<Report, ReportId> reportRepository) : base(httpContextAccessor)
    {
        _reportRepository = reportRepository;
        RuleFor(e => e)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to moderate this report.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(ApproveReportCommand command, CancellationToken cancellationToken)
    {
        if (!UserClaims.UserType.Equals(UserType.Moderator)) return false;
        return
            await _reportRepository.HasPermissionsToModerate(command.ReportId, UserClaims.ModeratorId,
                cancellationToken);

    }
}
