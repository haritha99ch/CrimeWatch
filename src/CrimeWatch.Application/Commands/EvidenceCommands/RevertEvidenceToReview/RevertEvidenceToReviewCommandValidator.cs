using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.EvidenceCommands.RevertEvidenceToReview;
public class RevertEvidenceToReviewCommandValidator : HttpContextValidator<RevertEvidenceToReviewCommand>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public RevertEvidenceToReviewCommandValidator(
        IHttpContextAccessor httpContextAccessor,
        IRepository<Evidence, EvidenceId> evidenceRepository) : base(httpContextAccessor)
    {
        _evidenceRepository = evidenceRepository;
        RuleFor(e => e)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to moderate this evidence.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(RevertEvidenceToReviewCommand command, CancellationToken cancellationToken)
    {
        if (!UserClaims.UserType.Equals(UserType.Moderator)) return false;
        return
            await _evidenceRepository.HasPermissionsToModerateAsync(command.EvidenceId, UserClaims.ModeratorId,
                cancellationToken);

    }
}
