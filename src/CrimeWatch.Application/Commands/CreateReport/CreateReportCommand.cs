using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.CreateReport;
public sealed record
    CreateReportCommand(
        WitnessId WitnessId,
        string Title,
        string Description,
        Location Location,
        MediaItemDto MediaItem
    ) : IRequest<Report>;
