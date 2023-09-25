namespace CrimeWatch.Application.Permissions.Commands.EvidenceCommands;
public class EditEvidencePermissionsHandler : RequestPermissions,
    IRequestHandler<EditEvidencePermissions, EvidencePermissions>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public EditEvidencePermissionsHandler(
            IHttpContextAccessor httpContextAccessor,
            IRepository<Evidence, EvidenceId> evidenceRepository
        ) : base(httpContextAccessor)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<EvidencePermissions> Handle(EditEvidencePermissions request, CancellationToken cancellationToken)
    {
        if (UserClaims.UserType.Equals(UserType.Guest)) return EvidencePermissions.Denied;
        var evidence = await _evidenceRepository.GetByIdAsync(
            request.EvidenceId,
            e => new { e.WitnessId, e.ModeratorId },
            cancellationToken);

        if (evidence == null) return EvidencePermissions.Denied;
        return UserClaims.UserType switch
        {
            UserType.Witness => CheckWitnessPermissions(evidence.WitnessId),
            UserType.Moderator => evidence.ModeratorId == null
                ? EvidencePermissions.Granted
                : CheckModeratorPermissions(evidence.ModeratorId),
            _ => EvidencePermissions.Denied
        };
    }

    private EvidencePermissions CheckModeratorPermissions(ModeratorId moderatorId)
        => UserClaims.ModeratorId == moderatorId ? EvidencePermissions.Granted : EvidencePermissions.Denied;

    private EvidencePermissions CheckWitnessPermissions(WitnessId witnessId)
        => UserClaims.WitnessId == witnessId ? EvidencePermissions.Granted : EvidencePermissions.Denied;
}
