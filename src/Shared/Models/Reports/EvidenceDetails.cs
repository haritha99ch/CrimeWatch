using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Shared.Models.Accounts;
using Shared.Models.MediaItems;

namespace Shared.Models.Reports;
public class EvidenceDetails : ISelector
{
    public required EvidenceId EvidenceId { get; set; }
    public WitnessDetailsForReportDetails? Author { get; set; }
    public ModeratorDetailsForReportDetails? Moderator { get; set; }
    public required string Caption { get; set; }
    public required string Description { get; set; }
    public required Location Location { get; set; }
    public required List<MediaViewItem> MediaItems { get; set; }
}
