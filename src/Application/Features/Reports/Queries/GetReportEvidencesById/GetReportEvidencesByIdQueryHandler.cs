namespace Application.Features.Reports.Queries.GetReportEvidencesById;
internal sealed class GetReportEvidencesByIdQueryHandler
    : IQueryHandler<GetReportEvidencesByIdQuery, List<EvidenceDetails>>
{
    private readonly IRepository<Report, ReportId> _reportRepository;
    private readonly IAuthenticationService _authenticationService;

    public GetReportEvidencesByIdQueryHandler(
            IRepository<Report, ReportId> reportRepository,
            IAuthenticationService authenticationService
        )
    {
        _reportRepository = reportRepository;
        _authenticationService = authenticationService;
    }
    public async Task<Result<List<EvidenceDetails>>> Handle(
            GetReportEvidencesByIdQuery request,
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
            .GetByIdAsync(request.ReportId, EvidenceDetails.SelectEnumerable(request.Pagination), cancellationToken);
        return reportEvidences ?? [];
    }
}
