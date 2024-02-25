using Application.Specifications.Reports;

namespace Application.Features.Reports.Queries.GetReportEvidencesByReportId;
internal sealed class GetReportEvidencesByReportIdQueryHandler
    : IQueryHandler<GetReportEvidencesByReportIdQuery, List<EvidenceDetails>>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IAuthenticationService _authenticationService;

    public GetReportEvidencesByReportIdQueryHandler(
            IRepository<Report, ReportId> reportRepository,
            IAuthenticationService authenticationService
        )
    {
        _reportRepository = reportRepository;
        _authenticationService = authenticationService;
    }
    public async Task<Result<List<EvidenceDetails>>> Handle(
            GetReportEvidencesByReportIdQuery request,
            CancellationToken cancellationToken
        )
    {
        var authenticationResult = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        var isModerator = false;
        AccountId? currentAccountId = default;

        authenticationResult.Handle(
            e =>
            {
                isModerator = e.IsModerator;
                currentAccountId = e.AccountId;
            }
        );
        var reportEvidences = await _reportRepository
            .GetManyAsync<EvidenceDetailsListByReportId, EvidenceDetails>(
                new(isModerator, request.ReportId, currentAccountId),
                cancellationToken);

        return reportEvidences;
    }
}
