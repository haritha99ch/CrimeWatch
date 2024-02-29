using Domain.AggregateModels.ReportAggregate.Entities;

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
        List<MediaUpload>? NewMediaItems = null
    ) : ICommand<EvidenceDetails>;
