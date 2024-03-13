using Application.Features.Accounts.Commands.CreateAccountForModerator;
using Application.Features.Accounts.Commands.CreateAccountForWitness;
using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.Entities;
using MediatR;
using Web.API.Helpers.Controllers;

namespace Web.API.Controllers;
[Route("api/[controller]/[action]")]
public class AccountsController : ControllerBase
{
    private readonly ISender _mediatr;

    public AccountsController(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost] [Route($"{nameof(Moderator)}")]
    public async Task<ActionResult<Account>> Create(
            [FromBody] CreateAccountForModeratorCommand command,
            CancellationToken cancellationToken
        )
    {
        var result = await _mediatr.Send(command, cancellationToken);
        return result.Handle(Ok, e => e.ToProblemDetails());
    }

    [HttpPost] [Route($"{nameof(Witness)}")]
    public async Task<ActionResult<Account>> Create(
            [FromBody] CreateAccountForWitnessCommand command,
            CancellationToken cancellationToken
        )
    {
        var result = await _mediatr.Send(command, cancellationToken);
        return result.Handle(Ok, e => e.ToProblemDetails());
    }

}
