using CrimeWatch.Application.Commands.ModeratorCommands.CreateModerator;
using CrimeWatch.Application.Commands.WitnessCommands.CreateWitness;
using CrimeWatch.Application.Queries.AccountQueries.GetAccount;

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
        return result is null ? NotFound("Account Not found!") : Ok(result);
    }

}
