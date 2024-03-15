using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Shared.Dto.Accounts;
using Shared.Dto.MediaItems;

namespace Shared.Dto.Reports;
public class EvidenceDetails
{
    public required EvidenceId EvidenceId { get; set; }
    public WitnessDetailsForReportDetails? Author { get; set; }
    public ModeratorDetailsForReportDetails? Moderator { get; set; }
    public required string Caption { get; set; }
    public required string Description { get; set; }
    public required Location Location { get; set; }
    public required List<MediaViewItem> MediaItems { get; set; }
}
