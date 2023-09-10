using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ReportCommands.ApproveReport;
public sealed record ApproveReportCommand(ReportId ReportId) : IRequest<Report>;
