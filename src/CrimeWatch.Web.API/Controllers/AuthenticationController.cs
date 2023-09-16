using CrimeWatch.Application.Commands.ModeratorCommands.CreateModerator;
using CrimeWatch.Application.Commands.WitnessCommands.CreateWitness;
using CrimeWatch.Application.Queries.AccountQueries.GetAccount;
using CrimeWatch.Application.Queries.AccountQueries.GetCurrentUser;

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

    [HttpGet("GetCurrentUser")]
    [Authorize]
    public async Task<ActionResult> GetCurrentUser()
    {
        var result = await _mediator.Send(new GetCurrentUserCommand());
        if (result is null) return NotFound("Account Not found!");
        if (result is Witness witness) return Ok(witness);
        if (result is Moderator moderator) return Ok(moderator);
        return BadRequest("Invalid Account Type");
    }

}
