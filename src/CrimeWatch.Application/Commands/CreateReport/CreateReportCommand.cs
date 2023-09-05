using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using Microsoft.AspNetCore.Http;

namespace CrimeWatch.Application.Commands.CreateReport;
public sealed record
    CreateReportCommand(
        WitnessId WitnessId,
        string Title,
        string Description,
        Location Location,
        IFormFile MediaItem
    ) : IRequest<Report>;
