using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetEvidences;
public sealed record GetAllEvidencesForReportQuery(ReportId ReportId) : IRequest<List<Evidence>>;
