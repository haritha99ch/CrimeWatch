﻿namespace CrimeWatch.Application.Queries.ReportQueries.GetAllModeratedReports;
public class GetAllModeratedReportsQueryHandler : IRequestHandler<GetAllModeratedReportsQuery, List<Report>>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetAllModeratedReportsQueryHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<List<Report>> Handle(GetAllModeratedReportsQuery request, CancellationToken cancellationToken)
        => await _reportRepository.GetAllModeratedReportsAsync(cancellationToken);
}
