using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.EvidenceQueries.GetEvidences;
public sealed record GetAllModeratedEvidenceForReportQuery(ReportId ReportId) : IRequest<List<Evidence>>;
