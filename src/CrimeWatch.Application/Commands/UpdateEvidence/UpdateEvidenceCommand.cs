using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.UpdateEvidence;
public sealed record
    UpdateEvidenceCommand(
        EvidenceId Id,
        string Title,
        string Description,
        Location Location,
        List<MediaItem> MediaItems,
        List<MediaItemDto> NewMediaItems
    ) : IRequest<Evidence>;
