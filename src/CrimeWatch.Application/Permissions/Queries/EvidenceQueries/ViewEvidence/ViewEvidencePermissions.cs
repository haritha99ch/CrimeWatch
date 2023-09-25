namespace CrimeWatch.Application.Permissions.Queries.EvidenceQueries.ViewEvidence;
public sealed record ViewEvidencePermissions(EvidenceId? EvidenceId = null) : IRequest<EvidencePermissions>;
