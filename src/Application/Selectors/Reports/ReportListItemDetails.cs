using Persistence.Common.Selectors;
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
    ) : Selector<Report, ReportAuthorizationInfo>, ISelector
{
    protected override Expression<Func<Report, ReportAuthorizationInfo>> SetProjection()
        => throw new NotImplementedException();
}
