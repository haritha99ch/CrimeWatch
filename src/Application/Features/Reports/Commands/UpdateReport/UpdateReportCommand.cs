using Domain.AggregateModels.ReportAggregate.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Reports.Commands.UpdateReport;
public sealed record UpdateReportCommand(
        ReportId ReportId,
        string Caption,
        string Description,
        string? No,
        string Street1,
        string? Street2,
        string City,
        string Province,
        List<ViolationType> ViolationTypes,
        MediaItem? MediaItem,
        IFormFile? NewMediaItem = null
    ) : ICommand<Report>;
