﻿using CrimeWatch.Application.Commands.ModeratorCommands.CreateModerator;
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

    [HttpPost($"{nameof(Witness)}/{nameof(SignUp)}")]
    public async Task<ActionResult<Witness>> SignUp([FromBody] CreateWitnessCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost($"{nameof(Moderator)}/{nameof(SignUp)}")]
    public async Task<ActionResult<Moderator>> SignUp([FromBody] CreateModeratorCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost(nameof(SignIn))]
    public async Task<ActionResult> SignIn([FromBody] GetAccountBySignInQuery query)
    {
        var result = await _mediator.Send(query);
        return result is null ? NotFound("Account Not found!") : Ok(result);
    }

    [HttpGet(nameof(GetCurrentUser))]
    [Authorize]
    public async Task<ActionResult> GetCurrentUser()
    {
        var result = await _mediator.Send(new GetCurrentUserCommand());
        return result.Match<ActionResult>(Ok, Ok, NotFound);
    }

}
