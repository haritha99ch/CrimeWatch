using Shared.Dto.MediaItems;
using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public record ReportListItemDetails(
        ReportId ReportId,
        WitnessDetailsForReportDetails? AuthorDetails,
        ModeratorDetailsForReportDetails? ModeratorDetails,
        string Caption,
        string Description,
        Location Location,
        Status Status,
        int BookmarksCount,
        bool IsBookmarkedByCurrentUser, // Handle in request handler
        MediaViewItem MediaItem
    ) : ISelector<Report, ReportAuthorizationInfo>
{
    public Expression<Func<Report, ReportAuthorizationInfo>> SetProjection()
        => throw new NotImplementedException();
}
