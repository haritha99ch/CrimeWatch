using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.AddEvidenceToReport;
public sealed record CreateEvidenceCommand(
        WitnessId AuthorId,
        ReportId ReportId,
        string Caption,
        string Description,
        Location Location,
        List<MediaItem>? MediaItems
    ) : IRequest<Evidence>;
