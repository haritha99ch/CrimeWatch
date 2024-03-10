using Application.Errors.Files;
using Application.Specifications.Reports;
using Persistence.Contracts.Services;

namespace Application.Features.Reports.Commands.UpdateReport;
internal sealed class UpdateReportCommandHandler : ICommandHandler<UpdateReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IFileStorageService _fileStorageService;

    public UpdateReportCommandHandler(
            IRepository<Report, ReportId> reportRepository,
            IFileStorageService fileStorageService
        )
    {
        _reportRepository = reportRepository;
        _fileStorageService = fileStorageService;
    }
    public async Task<Result<Report>> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.GetOneAsync(new ReportIncludingAll(request.ReportId), cancellationToken);
        if (report is null)
            return ReportNotFoundError.Create(
                message: $"Report with ReportID: {request.ReportId.Value.ToString()}, is not found");

        MediaUpload? newMediaUpload = default;
        Error? error = default;
        if (request.NewMediaItem is { } newMediaItem)
        {
            var uploadResult = await _fileStorageService.UploadFileAsync(
                request.ReportId.ToString(),
                newMediaItem,
                cancellationToken);
            var isUploadSucceed = uploadResult.Handle(e =>
                {
                    newMediaUpload = e;
                    return true;
                },
                e =>
                {
                    error = FileUploadError.Create(e.Message, e.InnerException?.Message ?? default);
                    return false;
                });
            if (!isUploadSucceed) return error!;

            var deleteResult = await _fileStorageService.DeleteFileAsync(
                report.Id.ToString(),
                report.MediaItem!.FileName,
                cancellationToken);
            if (!deleteResult) return FileDeleteError.Create();
        }

        report.Update(
            request.Caption,
            request.Description,
            request.No,
            request.Street1,
            request.Street2,
            request.City,
            request.Province,
            request.ViolationTypes,
            request.MediaItem,
            newMediaUpload);

        return await _reportRepository.UpdateAsync(report);
    }
}
