using Shared.Dto.MediaItems;

namespace Application.Selectors.Reports;
public record EvidenceDetails(
        EvidenceId EvidenceId,
        WitnessDetailsForReportDetails? Author,
        ModeratorDetailsForReportDetails? Moderator,
        string Caption,
        string Description,
        Location Location,
        List<MediaViewItem> MediaItems
    ) : ISelector;
