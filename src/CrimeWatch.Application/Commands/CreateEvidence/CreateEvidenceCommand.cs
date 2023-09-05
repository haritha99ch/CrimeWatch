using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.AddEvidenceToReport;
public sealed record CreateEvidenceCommand(
        WitnessId WitnessId,
        ReportId ReportId,
        string Caption,
        string Description,
        Location Location,
        List<MediaItemDto> MediaItems
    ) : IRequest<Evidence>;
