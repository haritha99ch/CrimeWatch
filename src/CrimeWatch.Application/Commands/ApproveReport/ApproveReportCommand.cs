using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ApproveReport;
public sealed record ApproveReportCommand(ReportId ReportId) : IRequest<Report>;
