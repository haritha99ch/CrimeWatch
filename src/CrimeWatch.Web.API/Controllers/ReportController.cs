using CrimeWatch.Application.Commands.ReportCommands.AddCommentToReport;
using CrimeWatch.Application.Commands.ReportCommands.ApproveReport;
using CrimeWatch.Application.Commands.ReportCommands.CreateReport;
using CrimeWatch.Application.Commands.ReportCommands.DeclineReport;
using CrimeWatch.Application.Commands.ReportCommands.DeleteReport;
using CrimeWatch.Application.Commands.ReportCommands.ModerateReport;
using CrimeWatch.Application.Commands.ReportCommands.RevertReportToReview;
using CrimeWatch.Application.Commands.ReportCommands.UpdateReport;
using CrimeWatch.Application.Queries.ReportQueries.GetAllReports;
using CrimeWatch.Application.Queries.ReportQueries.GetReport;

namespace CrimeWatch.Web.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(nameof(Create))]
    public async Task<ActionResult<Report>> Create([FromForm] CreateReportCommand command)
    {
        // Authorized
        var newReport = await _mediator.Send(command);
        return Ok(newReport);
    }

    [HttpGet(nameof(Get))]
    [AllowAnonymous]
    public async Task<ActionResult<List<Report>>> Get()
    {
        // Authorized
        // Filter moderated
        GetAllReportsQuery query = new();
        var reports = await _mediator.Send(query);

        return Ok(reports);
    }

    [HttpGet($"{nameof(Get)}/{{{nameof(reportId)}}}")]
    [AllowAnonymous]
    public async Task<ActionResult<Report>> Get([FromRoute] Guid reportId)
    {
        // Authorized
        // Filter moderated
        GetReportQuery query = new(new(reportId));
        var report = await _mediator.Send(query);
        return report != null
            ? Ok(report)
            : NotFound("Report Not Found!");
    }

    [HttpPatch(nameof(Update))]
    public async Task<ActionResult<Report>> Update([FromForm] UpdateReportCommand command)
    {
        // Authorized
        GetReportQuery getReportQuery = new(command.Id);
        var report = await _mediator.Send(getReportQuery);

        report = await _mediator.Send(command);
        return Ok(report);
    }

    [HttpPatch(nameof(Moderate))]
    public async Task<ActionResult<Report>> Moderate([FromBody] ModerateReportCommand command)
    {
        // Authorize
        var report = await _mediator.Send(command);
        return Ok(report);
    }

    [HttpPatch(nameof(Approve))]
    public async Task<ActionResult<Report>> Approve([FromBody] ApproveReportCommand command)
    {
        // Authorize
        var report = await _mediator.Send(command);

        return Ok(report);
    }

    [HttpPatch(nameof(Decline))]
    public async Task<ActionResult<Report>> Decline([FromBody] DeclineReportCommand command)
    {
        // Authorize
        var report = await _mediator.Send(command);

        return Ok(report);
    }

    [HttpPatch(nameof(Revert))]
    public async Task<ActionResult<Report>> Revert([FromBody] RevertReportToReviewCommand command)
    {
        // Authorize
        var report = await _mediator.Send(command);

        return Ok(report);
    }

    [HttpPost(nameof(Comment))]
    public async Task<ActionResult<Report>> Comment([FromBody] AddCommentToReportCommand command)
    {
        // Authorize
        var report = await _mediator.Send(command);

        return Ok(report);
    }

    [HttpDelete($"{nameof(Delete)}/{{{nameof(reportId)}}}")]
    public async Task<ActionResult<bool>> Delete([FromRoute] Guid reportId)
    {
        DeleteReportCommand command = new(new(reportId));
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
