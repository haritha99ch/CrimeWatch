using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Permissions.Queries.EvidenceQueries.ViewEvidence;
internal class ViewEvidencePermissionsHandler : RequestPermissions,
    IRequestHandler<ViewEvidencePermissions, EvidencePermissions>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public ViewEvidencePermissionsHandler(
        IHttpContextAccessor httpContextAccessor,
        IRepository<Evidence, EvidenceId> evidenceRepository) : base(httpContextAccessor)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<EvidencePermissions> Handle(ViewEvidencePermissions request, CancellationToken cancellationToken)
    {
        if (request.EvidenceId != null)
            return await GetPermissionToViewEvidenceById(request.EvidenceId, cancellationToken);
        return UserClaims.UserType switch
        {
            UserType.Guest => EvidencePermissions.Moderated,
            UserType.Witness => EvidencePermissions.Moderated,
            UserType.Moderator => EvidencePermissions.FullAccess,
            _ => throw new NotImplementedException()
        };
    }

    private async Task<EvidencePermissions> GetPermissionToViewEvidenceById(EvidenceId evidenceId,
        CancellationToken cancellationToken)
    {
        var report =
            await _evidenceRepository.GetByIdAsync(evidenceId, e => new { e.Status, e.WitnessId }, cancellationToken);
        if (report == null) return EvidencePermissions.Denied;
        return UserClaims.UserType switch
        {
            UserType.Witness =>
                report.WitnessId.Equals(UserClaims.WitnessId) ?
                    EvidencePermissions.Granted :
                    report.Status.Equals(Status.UnderReview) || report.Status.Equals(Status.Approved) ?
                        EvidencePermissions.Granted : EvidencePermissions.Denied,
            UserType.Moderator => EvidencePermissions.Granted,
            _ => EvidencePermissions.Denied
        };
    }
}
