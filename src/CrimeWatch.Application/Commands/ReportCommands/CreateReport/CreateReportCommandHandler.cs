using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.ReportCommands.CreateReport;
internal class CreateReportCommandHandler(
        IRepository<Report, ReportId> reportRepository,
        IFileStorageService fileStorageService)
    : IRequestHandler<CreateReportCommand, Report>
{

    public async Task<Report> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var mediaItem =
            await fileStorageService.SaveFileAsync(request.MediaItem, request.WitnessId, cancellationToken);

        var report = Report
            .Create(
                request.WitnessId,
                request.Title,
                request.Description,
                request.Location,
                mediaItem
            );

        var result = await reportRepository.AddAsync(report, cancellationToken);

        return result;
    }
}
