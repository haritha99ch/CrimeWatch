using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using CrimeWatch.Domain.ValueObjects;

namespace CrimeWatch.Shared.DTO;
public record ReportDto(
    WitnessId WitnessId,
    string Title,
    string Description,
    Location Location,
    MediaItemDto MediaItem,
    List<EvidenceDto>? Evidences = null
    );
