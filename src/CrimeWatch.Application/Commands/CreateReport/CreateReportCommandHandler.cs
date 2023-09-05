using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.CreateReport;
internal class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IFileStorageService _fileStorageService;

    public CreateReportCommandHandler(
        IRepository<Report, ReportId> reportRepository,
        IFileStorageService fileStorageService)
    {
        _reportRepository = reportRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Report> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var mediaItem =
            await _fileStorageService.SaveFileAsync(request.MediaItem, request.WitnessId, cancellationToken);

        Report report = Report
            .Create(
                request.WitnessId,
                request.Title,
                request.Description,
                request.Location,
                mediaItem
            );

        var result = await _reportRepository.AddAsync(report, cancellationToken);

        return result;
    }
}
