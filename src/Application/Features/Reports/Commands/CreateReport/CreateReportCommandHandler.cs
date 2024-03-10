using Application.Errors.Files;
using Persistence.Contracts.Services;

namespace Application.Features.Reports.Commands.CreateReport;
internal sealed class CreateReportCommandHandler : ICommandHandler<CreateReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IFileStorageService _fileStorageService;

    public CreateReportCommandHandler(
            IRepository<Report, ReportId> reportRepository,
            IFileStorageService fileStorageService
        )
    {
        _reportRepository = reportRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<Report>> Handle(
            CreateReportCommand request,
            CancellationToken cancellationToken
        )
    {
        var fileUploadResult = await _fileStorageService
            .UploadFileAsync(request.AuthorId.Value.ToString(), request.MediaItem, cancellationToken);
        MediaUpload mediaUpload = default!;
        Error error = default!;
        var successFullUpload = fileUploadResult.Handle(
            e =>
            {
                mediaUpload = e;
                return true;
            },
            e =>
            {
                error = FileUploadError.Create(e.Message, e.InnerException?.Message);
                return false;
            });
        if (!successFullUpload) return error;
        var report = Report.Create(
            request.AuthorId,
            request.Caption,
            request.Description,
            request.No,
            request.Street1,
            request.Street2,
            request.City,
            request.Province,
            request.ViolationTypes,
            mediaUpload);
        return await _reportRepository.AddAsync(report, cancellationToken);
    }
}
