namespace Application.Selectors.Reports;
public record ReportBookmarkInfo(Status ReportStatus, bool AlreadyBookmarked) : ISelector;
