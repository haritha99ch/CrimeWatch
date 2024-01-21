using Application.Helpers.Repositories;

namespace Application.Features.Reports.Queries.GetReportDetailsById;
internal sealed class GetReportDetailsByIdQueryHandler : IQueryHandler<GetReportDetailsByIdQuery, ReportDetails>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    public GetReportDetailsByIdQueryHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<ReportDetails>> Handle(
            GetReportDetailsByIdQuery request,
            CancellationToken cancellationToken
        )
    {
        var report = await _reportRepository.GetReportDetailsById(request.ReportId, cancellationToken);

        if (report is null) return ReportNotFoundError.Create(message: "Report not found!.");
        return report;
    }
}
