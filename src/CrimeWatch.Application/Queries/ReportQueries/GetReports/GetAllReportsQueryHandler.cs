﻿using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.ReportQueries.GetReports;
internal class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, List<Report>>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetAllReportsQueryHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<List<Report>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        => await _reportRepository.GetAllReportsWithMediaItemModeratorAndWitnessByAsync(cancellationToken);
}
