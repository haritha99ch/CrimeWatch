using Persistence.Common.Utilities;

namespace Application.Features.Reports.Queries.GetReportEvidencesByReportId;
public sealed record GetReportEvidencesByReportIdQuery(ReportId ReportId, Pagination? Pagination = null)
    : IQuery<List<EvidenceDetails>>;
