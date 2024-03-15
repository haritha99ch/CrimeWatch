using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;
using Shared.Dto.Accounts;
using Shared.Dto.MediaItems;

namespace Shared.Dto.Reports;
public class ReportDetails
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
