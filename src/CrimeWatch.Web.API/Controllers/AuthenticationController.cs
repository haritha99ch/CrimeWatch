using CrimeWatch.Application.Commands.ModeratorCommands.CreateModerator;
using CrimeWatch.Application.Commands.WitnessCommands.CreateWitness;
using CrimeWatch.Application.Queries.AccountQueries.GetAccount;
using CrimeWatch.Application.Queries.ModeratorQueries.GetModerator;
using CrimeWatch.Application.Queries.WitnessQueries.GetWitness;

namespace CrimeWatch.Web.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Witness/SignUp")]
    public async Task<ActionResult<Witness>> SignUp([FromBody] CreateWitnessCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("Moderator/SignUp")]
    public async Task<ActionResult<Moderator>> SignUp([FromBody] CreateModeratorCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("SignIn")]
    public async Task<ActionResult> SignIn([FromBody] GetAccountBySignInQuery query)
    {
        var result = await _mediator.Send(query);
        if (result is null) return NotFound("Account Not found!");

        if (result.IsModerator)
        {
            var getModerator = new GetModeratorByAccountIdQuery(result.Id);
            var moderator = await _mediator.Send(getModerator);
            return Ok(moderator);
        }

        var getWitness = new GetWitnessByAccountIdQuery(result.Id);
        var witness = await _mediator.Send(getWitness);
        return Ok(witness);
    }

}
