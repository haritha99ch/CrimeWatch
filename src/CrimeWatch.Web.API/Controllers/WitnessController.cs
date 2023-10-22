using CrimeWatch.Application.Commands.WitnessCommands.DeleteWitness;
using CrimeWatch.Application.Commands.WitnessCommands.UpdateWitness;
using CrimeWatch.Application.Queries.ReportQueries.GetWitnessReports;
using CrimeWatch.Application.Queries.WitnessQueries.GetWitness;

namespace CrimeWatch.Web.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WitnessController : ControllerBase
{
    private readonly IMediator _mediator;

    public WitnessController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet($"{nameof(Get)}/{{{nameof(witnessId)}}}")]
    public async Task<ActionResult<Witness>> Get([FromRoute] Guid witnessId)
    {
        GetWitnessByIdQuery query = new(new(witnessId));
        var witness = await _mediator.Send(query);
        return witness != null
            ? Ok(witness)
            : NotFound("Witness Not Found!");
    }

    [HttpGet($"{nameof(GetReports)}/{{{nameof(witnessId)}}}")]
    public async Task<ActionResult<List<Report>>> GetReports([FromRoute] Guid witnessId)
    {
        GetWitnessReportsQuery query = new(new(witnessId));
        var reports = await _mediator.Send(query);
        return Ok(reports);
    }

    [HttpPatch(nameof(Update))]
    public async Task<ActionResult<Witness>> Update([FromBody] UpdateWitnessCommand command)
    {
        // Authorized
        var witness = await _mediator.Send(command);
        return Ok(witness);
    }

    [HttpDelete($"{nameof(Delete)}/{{{nameof(witnessId)}}}")]
    public async Task<ActionResult<bool>> Delete([FromRoute] Guid witnessId)
    {
        DeleteWitnessCommand command = new(new(witnessId));
        var result = await _mediator.Send(command);
        return result ? Ok(result) : BadRequest("Could not delete");
    }
}
