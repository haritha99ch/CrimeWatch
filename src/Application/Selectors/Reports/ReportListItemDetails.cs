using Shared.Dto.MediaItems;

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
    );
