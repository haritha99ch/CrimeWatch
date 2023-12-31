﻿using CrimeWatch.Application.Commands.EvidenceCommands.AddCommentToEvidence;
using CrimeWatch.Application.Commands.EvidenceCommands.ApproveEvidence;
using CrimeWatch.Application.Commands.EvidenceCommands.CreateEvidence;
using CrimeWatch.Application.Commands.EvidenceCommands.DeclineEvidence;
using CrimeWatch.Application.Commands.EvidenceCommands.DeleteEvidence;
using CrimeWatch.Application.Commands.EvidenceCommands.ModerateEvidence;
using CrimeWatch.Application.Commands.EvidenceCommands.RevertEvidenceToReview;
using CrimeWatch.Application.Commands.EvidenceCommands.UpdateEvidence;
using CrimeWatch.Application.Queries.EvidenceQueries.GetAllEvidencesForReport;

namespace CrimeWatch.Web.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EvidenceController : ControllerBase
{
    private readonly IMediator _mediator;

    public EvidenceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(nameof(Create))]
    public async Task<ActionResult<Evidence>> Create([FromForm] CreateEvidenceCommand command)
    {
        // Authorize
        var newEvidence = await _mediator.Send(command);

        return Ok(newEvidence);
    }

    [HttpGet($"{nameof(GetAllForReport)}/{{{nameof(reportId)}}}")]
    public async Task<ActionResult<List<Evidence>>> GetAllForReport([FromRoute] Guid reportId)
    {
        // Authorize
        GetAllEvidencesForReportQuery query = new(new(reportId));

        var evidence = await _mediator.Send(query);
        return Ok(evidence);
    }

    [HttpGet($"{nameof(Get)}/{{{nameof(evidenceId)}}}")]
    public Task<ActionResult<Evidence>> Get([FromRoute] Guid evidenceId) => throw new NotImplementedException();

    [HttpPatch(nameof(Update))]
    public async Task<ActionResult<Evidence>> Update([FromForm] UpdateEvidenceCommand command)
    {
        // Authorized
        var updatedEvidence = await _mediator.Send(command);

        return Ok(updatedEvidence);
    }

    [HttpPatch(nameof(Moderate))]
    public async Task<ActionResult<Evidence>> Moderate([FromBody] ModerateEvidenceCommand command)
    {
        // Authorize
        var evidence = await _mediator.Send(command);
        return Ok(evidence);
    }

    [HttpPatch(nameof(Approve))]
    public async Task<ActionResult<Evidence>> Approve([FromBody] ApproveEvidenceCommand command)
    {
        // Authorize
        var evidence = await _mediator.Send(command);

        return Ok(evidence);
    }

    [HttpPatch(nameof(Decline))]
    public async Task<ActionResult<Evidence>> Decline([FromBody] DeclineEvidenceCommand command)
    {
        // Authorize
        var evidence = await _mediator.Send(command);

        return Ok(evidence);
    }

    [HttpPatch(nameof(Revert))]
    public async Task<ActionResult<Evidence>> Revert([FromBody] RevertEvidenceToReviewCommand command)
    {
        // Authorize
        var evidence = await _mediator.Send(command);

        return Ok(evidence);
    }

    [HttpPost(nameof(Comment))]
    public async Task<ActionResult<Evidence>> Comment([FromBody] AddCommentToEvidenceCommand command)
    {
        // Authorize
        var evidence = await _mediator.Send(command);

        return Ok(evidence);
    }

    [HttpDelete($"{nameof(Delete)}/{{{nameof(evidenceId)}}}")]
    public async Task<ActionResult<bool>> Delete([FromRoute] Guid evidenceId)
    {
        DeleteEvidenceCommand command = new(new(evidenceId));
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
