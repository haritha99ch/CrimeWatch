using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.EvidenceCommands.DeleteEvidence;
public class DeleteEvidenceCommandValidator : HttpContextValidator<DeleteEvidenceCommand>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public DeleteEvidenceCommandValidator(
        IHttpContextAccessor httpContextAccessor,
        IRepository<Evidence, EvidenceId> evidenceRepository) : base(httpContextAccessor)
    {
        _evidenceRepository = evidenceRepository;
        RuleFor(e => e)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to update this evidence.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(DeleteEvidenceCommand command, CancellationToken cancellationToken)
        => UserClaims.WitnessId != null
            && await _evidenceRepository.HasPermissionsToEditAsync(command.Id, UserClaims.WitnessId, cancellationToken);
}
