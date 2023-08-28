using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.UpdateReport;
public sealed record
    UpdateReportCommand(
        ReportId Id,
        string Title,
        string Description,
        Location Location,
        MediaItem MediaItem
    ) : IRequest<Report>;
