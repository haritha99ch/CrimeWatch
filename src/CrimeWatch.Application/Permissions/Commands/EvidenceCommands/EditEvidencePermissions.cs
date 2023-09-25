namespace CrimeWatch.Application.Permissions.Commands.EvidenceCommands;
public sealed record EditEvidencePermissions(EvidenceId EvidenceId) : IRequest<EvidencePermissions>;
