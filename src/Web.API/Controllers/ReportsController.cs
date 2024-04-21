using Application.Features.Reports.Commands.CreateReport;
using Application.Features.Reports.Queries.GetReports;
using MediatR;
using Shared.Models.Reports;
using Web.API.Helpers.Controllers;

namespace Web.API.Controllers;
[Route("api/[controller]/[action]")]
public class ReportsController : ControllerBase
{
    private readonly ISender _mediatr;

    public ReportsController(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost]
    public async Task<ActionResult<ReportDetails>> Create(
            CreateReportCommand command,
            CancellationToken cancellationToken
        )
    {
        var result = await _mediatr.Send(command, cancellationToken);
        return result.Handle(Ok, e => e.ToProblemDetails());
    }

    [HttpGet]
    public async Task<ActionResult<List<ReportDetails>>> Get(GetReportsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(query, cancellationToken);
        return result.Handle(Ok, e => e.ToProblemDetails());
    }
}
