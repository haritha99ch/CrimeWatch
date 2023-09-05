using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.CreateReport;
internal class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public CreateReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Report> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var mediaItem = MediaItem.Create(request.MediaItem.Type, "url from file");
        Report report = Report
            .Create(
                request.WitnessId,
                request.Title,
                request.Description,
                request.Location,
                mediaItem
            );

        return await _reportRepository.AddAsync(report, cancellationToken);
    }
}
