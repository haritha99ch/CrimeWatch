using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.CreateReport;
public sealed record
    CreateReportCommand(
        WitnessId AuthorId,
        string Title,
        string Description,
        Location Location,
        MediaItem MediaItem,
        List<Evidence>? Evidences
    ) : IRequest<Report>;
