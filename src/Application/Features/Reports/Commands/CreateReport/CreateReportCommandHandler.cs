namespace Application.Features.Reports.Commands.CreateReport;

internal sealed class CreateReportCommandHandler : ICommandHandler<CreateReportCommand, Report>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public CreateReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<Report>> Handle(
        CreateReportCommand request,
        CancellationToken cancellationToken
    )
    {
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
            request.MediaItem
        );
        return await _reportRepository.AddAsync(report, cancellationToken);
    }
}
