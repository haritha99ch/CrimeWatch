namespace Application.Features.Reports.Queries.GetReportDetailsById;
public record GetReportDetailsByIdQuery(ReportId ReportId) : IQuery<ReportDetails>;
