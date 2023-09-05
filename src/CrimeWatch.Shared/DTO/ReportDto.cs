using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Shared.DTO;
public record ReportDto(
    WitnessId WitnessId,
    string Title,
    string Description,
    Location Location,
    MediaItemDto MediaItem,
    List<EvidenceDto>? Evidences = null
    );
