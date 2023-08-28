using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.UpdateEvidence;
public sealed record
    UpdateEvidenceCommand(
        EvidenceId Id,
        string Caption,
        string Description,
        Location Location,
        List<MediaItem>? MediaItems
    ) : IRequest<Evidence>;
