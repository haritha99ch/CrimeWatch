using Application.Features.Reports.Commands.CreateReport;
using MediatR;
using Shared.Dto.Reports;
using Web.API.Helpers.Controllers;

namespace Web.API.Controllers;
public class ReportsController : ControllerBase
{
    private readonly ISender _mediatr;

    public ReportsController(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost]
    public async Task<ActionResult<ReportDetails>> Create(
            [FromBody] CreateReportCommand command,
            CancellationToken cancellationToken
        )
    {
        var result = await _mediatr.Send(command, cancellationToken);
        return result.Handle(Ok, e => e.ToProblemDetails());
    }
}
