using CrimeWatch.Application.Commands.AddCommentToEvidence;
using CrimeWatch.Application.Commands.AddEvidenceToReport;
using CrimeWatch.Application.Commands.ApproveEvidence;
using CrimeWatch.Application.Commands.DeclineEvidence;
using CrimeWatch.Application.Commands.DeleteEvidence;
using CrimeWatch.Application.Commands.ModerateEvidence;
using CrimeWatch.Application.Commands.RevertEvidenceToReview;
using CrimeWatch.Application.Commands.UpdateEvidence;
using CrimeWatch.Application.Queries.GetEvidences;

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

    [HttpPost("Create")]
    public async Task<ActionResult<Evidence>> Create([FromBody] EvidenceDto evidence)
    {
        // Authorize
        List<MediaItem> mediaItems = new();

        if (evidence.MediaItems != null)
        {
            foreach (var mediaItem in evidence.MediaItems)
            {
                // TODO: File hosting operation
                mediaItems.Add(MediaItem.Create(mediaItem.Type, "url from file"));
            }
        }

        CreateEvidenceCommand command = new(
                evidence.WitnessId!,
                evidence.ReportId!,
                evidence.Title,
                evidence.Description,
                evidence.Location,
                mediaItems
            );

        var newEvidence = await _mediator.Send(command);

        return Ok(newEvidence);
    }

    [HttpGet("GetAllForReport/{reportId}")]
    public async Task<ActionResult<List<Evidence>>> GetAllForReport([FromRoute] Guid reportId)
    {
        // Authorize
        GetAllEvidencesForReportQuery query = new(new(reportId));

        var evidence = await _mediator.Send(query);
        return Ok(evidence);
    }

    [HttpGet("Get/{evidenceId}")]
    public Task<ActionResult<Evidence>> Get([FromRoute] Guid evidenceId)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("Update")]
    public async Task<ActionResult<Report>> Update([FromBody] EvidenceDto evidence)
    {
        // Authorized
        List<MediaItem> newMediaItems = new();
        if (evidence.NewMediaItems != null)
        {
            foreach (var mediaItem in evidence.NewMediaItems)
            {
                // TODO: Hosting action
                MediaItem item = MediaItem.Create(mediaItem.Type, "New Url");
                newMediaItems.Add(item);
            }
        }

        List<MediaItem> existingMediaItems = new();
        if (evidence.MediaItems != null)
        {
            foreach (var mediaItem in evidence.MediaItems)
            {
                // TODO: Hosting action
                MediaItem item = MediaItem.Create(mediaItem.Type, "New Url");
                item.Id = mediaItem.Id!;
                existingMediaItems.Add(item);
            }
        }

        UpdateEvidenceCommand command = new(
                evidence.Id!,
                evidence.Title,
                evidence.Description,
                evidence.Location,
                existingMediaItems,
                newMediaItems
            );
        var updatedEvidence = await _mediator.Send(command);

        return Ok(updatedEvidence);
    }

    [HttpPatch("Moderate")]
    public async Task<ActionResult<Report>> Moderate([FromBody] ModerateEvidenceCommand command)
    {
        // Authorize
        var evidence = await _mediator.Send(command);
        return Ok(evidence);
    }

    [HttpPatch("Approve")]
    public async Task<ActionResult<Report>> Approve([FromBody] ApproveEvidenceCommand command)
    {
        // Authorize
        var evidence = await _mediator.Send(command);

        return Ok(evidence);
    }

    [HttpPatch("Decline")]
    public async Task<ActionResult<Report>> Decline([FromBody] DeclineEvidenceCommand command)
    {
        // Authorize
        var evidence = await _mediator.Send(command);

        return Ok(evidence);
    }

    [HttpPatch("Revert")]
    public async Task<ActionResult<Report>> Revert([FromBody] RevertEvidenceToReviewCommand command)
    {
        // Authorize
        var evidence = await _mediator.Send(command);

        return Ok(evidence);
    }

    [HttpPost("Comment")]
    public async Task<ActionResult<Report>> Comment([FromBody] AddCommentToEvidenceCommand command)
    {
        // Authorize
        var evidence = await _mediator.Send(command);

        return Ok(evidence);
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult<bool>> Delete([FromBody] DeleteEvidenceCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }


}
