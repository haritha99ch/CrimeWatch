using Persistence.Common.Specifications.Types;

namespace Application.Features.Reports.Queries.GetReportEvidencesByReportId;
public sealed record GetReportEvidencesByReportIdQuery(ReportId ReportId, Pagination? Pagination = null)
    : IQuery<List<EvidenceDetails>>;
