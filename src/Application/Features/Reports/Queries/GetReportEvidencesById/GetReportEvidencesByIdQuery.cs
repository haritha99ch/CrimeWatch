using Persistence.Common.Specifications.Types;

namespace Application.Features.Reports.Queries.GetReportEvidencesById;
public sealed record GetReportEvidencesByIdQuery(ReportId ReportId, Pagination? Pagination)
    : IQuery<List<EvidenceDetails>>;
