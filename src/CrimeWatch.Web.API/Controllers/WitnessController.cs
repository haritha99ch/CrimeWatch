﻿using CrimeWatch.Application.Commands.WitnessCommands.DeleteWitness;
using CrimeWatch.Application.Commands.WitnessCommands.UpdateWitness;
using CrimeWatch.Application.Queries.ReportQueries.GetReports;
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

    [HttpGet("Get/{witnessId}")]
    public async Task<ActionResult<Witness>> Get([FromRoute] Guid witnessId)
    {
        GetWitnessByIdQuery query = new(new(witnessId));
        var witness = await _mediator.Send(query);
        return witness != null
            ? Ok(witness)
            : NotFound("Witness Not Found!");
    }

    [HttpGet("GetReports/{witnessId}")]
    public async Task<ActionResult<List<Report>>> GetReports([FromRoute] Guid witnessId)
    {
        GetWitnessReportQuery query = new(new(witnessId));
        var reports = await _mediator.Send(query);
        return Ok(reports);
    }

    [HttpPatch("Update")]
    public async Task<ActionResult<Witness>> Update([FromBody] UpdateWitnessCommand command)
    {
        // Authorized
        var witness = await _mediator.Send(command);
        return Ok(witness);
    }

    [HttpDelete("Delete/{witnessId}")]
    public async Task<ActionResult<bool>> Delete([FromRoute] Guid witnessId)
    {
        DeleteWitnessCommand command = new(new(witnessId));
        var result = await _mediator.Send(command);
        return result ? Ok(result) : BadRequest("Could not delete");
    }
}
