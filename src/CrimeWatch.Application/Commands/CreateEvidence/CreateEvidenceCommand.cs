using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using Microsoft.AspNetCore.Http;

namespace CrimeWatch.Application.Commands.AddEvidenceToReport;
public sealed record CreateEvidenceCommand(
        WitnessId WitnessId,
        ReportId ReportId,
        string Caption,
        string Description,
        Location Location,
        List<IFormFile> MediaItems
    ) : IRequest<Evidence>;
