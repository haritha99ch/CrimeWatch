using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using CrimeWatch.Domain.ValueObjects;

namespace CrimeWatch.Shared.DTO;
public record EvidenceDto(
    WitnessId WitnessId,
    ReportId? ReportId,
    string Caption,
    string Description,
    Location Location,
    List<MediaItemDto>? MediaItems
);
