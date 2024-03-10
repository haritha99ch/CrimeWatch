using Persistence.Contracts.Services;

namespace Application.Features.Reports.Commands.DeleteReport;
public sealed class DeleteReportCommandHandler : ICommandHandler<DeleteReportCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IFileStorageService _fileStorageService;

    public DeleteReportCommandHandler(
            IRepository<Report, ReportId> reportRepository,
            IFileStorageService fileStorageService
        )
    {
        _reportRepository = reportRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<bool>> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _reportRepository.DeleteByIdAsync(request.ReportId, cancellationToken);
        if (!deleted) return ReportCouldNotBeDeletedError.Create();

        await _fileStorageService.DeleteContainerAsync(request.ReportId.ToString(), cancellationToken);

        return true;
    }
}
