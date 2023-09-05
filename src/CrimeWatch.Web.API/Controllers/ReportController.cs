using CrimeWatch.Application.Commands.AddCommentToReport;
using CrimeWatch.Application.Commands.ApproveReport;
using CrimeWatch.Application.Commands.CreateReport;
using CrimeWatch.Application.Commands.DeclineReport;
using CrimeWatch.Application.Commands.DeleteReport;
using CrimeWatch.Application.Commands.ModerateReport;
using CrimeWatch.Application.Commands.RevertReportToReview;
using CrimeWatch.Application.Commands.UpdateReport;
using CrimeWatch.Application.Queries.GetReport;
using CrimeWatch.Application.Queries.GetReports;

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

    [HttpPost("Create")]
    public async Task<ActionResult<Report>> Create([FromForm] CreateReportCommand command)
    {
        // Authorized
        var newReport = await _mediator.Send(command);
        return Ok(newReport);
    }

    [HttpGet("Get")]
    public async Task<ActionResult<List<Report>>> Get()
    {
        // Authorized
        // Filter moderated
        GetAllReportsQuery query = new();
        var reports = await _mediator.Send(query);

        return Ok(reports);
    }

    [HttpGet("Get/{reportId}")]
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

    [HttpPatch("Update")]
    public async Task<ActionResult<Report>> Update([FromForm] UpdateReportCommand command)
    {
        // Authorized
        GetReportQuery getReportQuery = new(command.Id);
        var report = await _mediator.Send(getReportQuery);

        report = await _mediator.Send(command);
        return Ok(report);
    }

    [HttpPatch("Moderate")]
    public async Task<ActionResult<Report>> Moderate([FromBody] ModerateReportCommand command)
    {
        // Authorize
        var report = await _mediator.Send(command);
        return Ok(report);
    }

    [HttpPatch("Approve")]
    public async Task<ActionResult<Report>> Approve([FromBody] ApproveReportCommand command)
    {
        // Authorize
        var report = await _mediator.Send(command);

        return Ok(report);
    }

    [HttpPatch("Decline")]
    public async Task<ActionResult<Report>> Decline([FromBody] DeclineReportCommand command)
    {
        // Authorize
        var report = await _mediator.Send(command);

        return Ok(report);
    }

    [HttpPatch("Revert")]
    public async Task<ActionResult<Report>> Revert([FromBody] RevertReportToReviewCommand command)
    {
        // Authorize
        var report = await _mediator.Send(command);

        return Ok(report);
    }

    [HttpPost("Comment")]
    public async Task<ActionResult<Report>> Comment([FromBody] AddCommentToReportCommand command)
    {
        // Authorize
        var report = await _mediator.Send(command);

        return Ok(report);
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult<bool>> Delete([FromBody] DeleteReportCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
