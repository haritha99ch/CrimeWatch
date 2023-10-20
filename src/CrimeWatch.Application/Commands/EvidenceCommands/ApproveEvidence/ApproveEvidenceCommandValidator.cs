namespace CrimeWatch.Application.Commands.EvidenceCommands.ApproveEvidence;
public class ApproveEvidenceCommandValidator : HttpContextValidator<ApproveEvidenceCommand>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public ApproveEvidenceCommandValidator(
        IHttpContextAccessor httpContextAccessor,
        IRepository<Evidence, EvidenceId> evidenceRepository) : base(httpContextAccessor)
    {
        _evidenceRepository = evidenceRepository;
        RuleFor(e => e)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to moderate this evidence.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(ApproveEvidenceCommand command, CancellationToken cancellationToken)
    {
        if (!UserClaims.UserType.Equals(UserType.Moderator)) return false;
        return
            await _evidenceRepository.HasPermissionsToModerateAsync(command.EvidenceId, UserClaims.ModeratorId,
                cancellationToken);

    }
}
