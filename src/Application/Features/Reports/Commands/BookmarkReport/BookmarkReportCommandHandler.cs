namespace Application.Features.Reports.Commands.BookmarkReport;
internal sealed class BookmarkReportCommandHandler : ICommandHandler<BookmarkReportCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public BookmarkReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(BookmarkReportCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.AsTracking().GetByIdAsync(request.ReportId, cancellationToken);
        report!.AddBookmark(request.AccountId);
        report = await _reportRepository.UpdateAsync(report, cancellationToken);
        return report.Bookmarks.Any(e => e.AccountId.Equals(request.AccountId));
    }
}
