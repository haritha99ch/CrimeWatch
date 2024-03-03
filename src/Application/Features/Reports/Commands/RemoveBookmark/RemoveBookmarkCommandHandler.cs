namespace Application.Features.Reports.Commands.RemoveBookmark;
internal sealed class RemoveBookmarkCommandHandler : ICommandHandler<RemoveBookmarkCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public RemoveBookmarkCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(RemoveBookmarkCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.AsTracking().GetByIdAsync(request.ReportId, cancellationToken);
        report!.RemoveBookmark(request.AccountId);
        report = await _reportRepository.UpdateAsync(report, cancellationToken);
        return !report.Bookmarks.Any(e => e.AccountId.Equals(request.AccountId));
    }
}
