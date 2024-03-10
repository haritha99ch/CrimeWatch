using Domain.AggregateModels.ReportAggregate.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Reports.Commands.UpdateEvidence;
public sealed record UpdateEvidenceCommand(
        ReportId ReportId,
        EvidenceId EvidenceId,
        string Caption,
        string Description,
        string No,
        string Street1,
        string Street2,
        string City,
        string Province,
        List<MediaItem>? MediaItems,
        IEnumerable<IFormFile>? NewMediaItems = null
    ) : ICommand<EvidenceDetails>;
