using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetEvidences;
public sealed record GetAllEvidencesForReportCommand(ReportId ReportId) : IRequest<List<Evidence>>;
