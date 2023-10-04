using CrimeWatch.Application.Commands.ModeratorCommands.DeleteModerator;
using CrimeWatch.Application.Commands.ModeratorCommands.UpdateModerator;
using CrimeWatch.Application.Queries.ModeratorQueries.GetModerator;
using CrimeWatch.Application.Queries.ReportQueries.GetModeratorReports;

namespace CrimeWatch.Web.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ModeratorController : ControllerBase
{
    private readonly IMediator _mediator;

    public ModeratorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Get/{moderatorId}")]
    public async Task<ActionResult<Moderator>> Get([FromRoute] Guid moderatorId)
    {
        // Authorized
        GetModeratorByIdQuery query = new(new(moderatorId));
        var moderator = await _mediator.Send(query);
        return moderator != null
            ? Ok(moderator)
            : NotFound("Moderator Not Found!");
    }

    [HttpGet("GetReports/{moderatorId}")]
    public async Task<ActionResult<List<Report>>> GetReports([FromRoute] Guid moderatorId)
    {
        // Authorized
        GetModeratorReportsQuery query = new(new(moderatorId));
        var reports = await _mediator.Send(query);
        return Ok(reports);
    }

    [HttpPatch("Update")]
    public async Task<ActionResult<Witness>> Update([FromBody] UpdateModeratorCommand command)
    {
        // Authorized
        var moderator = await _mediator.Send(command);
        return Ok(moderator);
    }

    [HttpDelete("Delete/{moderatorId}")]
    public async Task<ActionResult<bool>> Delete([FromRoute] Guid moderatorId)
    {
        DeleteModeratorCommand command = new(new(moderatorId));
        var result = await _mediator.Send(command);
        return result ? Ok(result) : BadRequest("Could not delete");
    }
}
