using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Shared.DTO;
public record EvidenceDto(
    EvidenceId Id,
    WitnessId? WitnessId,
    ReportId? ReportId,
    string Title,
    string Description,
    Location Location,
    List<MediaItemDto>? MediaItems,
    List<MediaItemDto>? NewMediaItems
);
