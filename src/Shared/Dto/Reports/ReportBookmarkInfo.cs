using Domain.AggregateModels.ReportAggregate.Enums;

namespace Shared.Dto.Reports;
public class ReportBookmarkInfo
{
    public Status ReportStatus { get; set; }
    public bool AlreadyBookmarked { get; set; }
}
