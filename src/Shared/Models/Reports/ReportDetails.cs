using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Shared.Models.Accounts;
using Shared.Models.MediaItems;

namespace Shared.Models.Reports;
public class ReportDetails : ISelector
{
    public required ReportId ReportId { get; set; }
    public WitnessDetailsForReportDetails? AuthorDetails { get; set; }
    public ModeratorDetailsForReportDetails? ModeratorDetails { get; set; }
    public required string Caption { get; set; }
    public required string Description { get; set; }
    public required Location Location { get; set; }
    public Status Status { get; set; }
    public int BookmarksCount { get; set; }
    public bool IsBookmarkedByCurrentUser { get; set; }
    public MediaViewItem? MediaItem { get; set; }
}
