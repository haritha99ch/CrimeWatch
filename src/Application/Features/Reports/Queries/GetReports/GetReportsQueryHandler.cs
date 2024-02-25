using Application.Specifications.Reports;

namespace Application.Features.Reports.Queries.GetReports;
internal sealed class GetReportsQueryHandler : IQueryHandler<GetReportsQuery, List<ReportDetails>>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public GetReportsQueryHandler(
            IAuthenticationService authenticationService,
            IRepository<Report, ReportId> reportRepository
        )
    {
        _authenticationService = authenticationService;
        _reportRepository = reportRepository;
    }

    public async Task<Result<List<ReportDetails>>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
    {
        var authenticationResult = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        var isModerator = false;
        AccountId? currentAccountId = null;
        authenticationResult.Handle(
            e =>
            {
                currentAccountId = e.AccountId;
                isModerator = e.IsModerator;
            }
        );

        var reports =
            await _reportRepository.GetManyAsync<ReportDetailsList, ReportDetails>(
                new(isModerator, currentAccountId),
                cancellationToken);

        return reports;
    }
}
