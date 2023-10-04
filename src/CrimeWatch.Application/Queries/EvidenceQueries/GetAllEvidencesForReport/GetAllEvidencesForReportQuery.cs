using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.EvidenceQueries.GetAllEvidencesForReport;
public record GetAllEvidencesForReportQuery(ReportId ReportId) : IRequest<List<Evidence>>;
