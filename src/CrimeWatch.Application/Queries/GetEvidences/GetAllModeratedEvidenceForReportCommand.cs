using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetEvidences;
public sealed record GetAllModeratedEvidenceForReportCommand(ReportId ReportId) : IRequest<List<Evidence>>;
