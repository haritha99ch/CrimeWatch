using Domain.AggregateModels.ReportAggregate.Enums;

namespace Shared.Models.Reports;
public class ReportBookmarkInfo : ISelector
{
    public Status ReportStatus { get; set; }
    public bool AlreadyBookmarked { get; set; }
}
