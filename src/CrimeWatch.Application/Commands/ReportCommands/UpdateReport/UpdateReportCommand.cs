using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using Microsoft.AspNetCore.Http;

namespace CrimeWatch.Application.Commands.ReportCommands.UpdateReport;
public sealed record
    UpdateReportCommand(
        ReportId Id,
        string Title,
        string Description,
        Location Location,
        MediaItem? MediaItem = null,
        IFormFile? NewMediaItem = null
    ) : IRequest<Report>;
