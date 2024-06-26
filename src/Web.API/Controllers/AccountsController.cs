﻿using Application.Features.Accounts.Commands.CreateAccountForModerator;
using Application.Features.Accounts.Commands.CreateAccountForWitness;
using Application.Features.Accounts.Queries.GetAccountById;
using Application.Features.Accounts.Queries.GetAccountInfoById;
using Application.Features.Accounts.Queries.SignInToAccount;
using Domain.AggregateModels.AccountAggregate.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Shared.Models.Accounts;
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
    public async Task<ActionResult<AccountInfo>> Create(
            [FromBody] CreateAccountForModeratorCommand command,
            CancellationToken cancellationToken
        )
    {
        var result = await _mediatr.Send(command, cancellationToken);
        return result.Handle(Ok, e => e.ToProblemDetails());
    }

    [HttpPost] [Route($"{nameof(Witness)}")]
    public async Task<ActionResult<AccountInfo>> Create(
            [FromBody] CreateAccountForWitnessCommand command,
            CancellationToken cancellationToken
        )
    {
        var result = await _mediatr.Send(command, cancellationToken);
        return result.Handle(Ok, e => e.ToProblemDetails());
    }

    [HttpPost]
    public async Task<ActionResult<string>> SignIn(
            [FromBody] SignInToAccountQuery query,
            CancellationToken cancellationToken
        )
    {
        var result = await _mediatr.Send(query, cancellationToken);
        return result.Handle(Ok, e => e.ToProblemDetails());
    }

    [HttpGet]
    public async Task<ActionResult<AccountInfo>> GetInfoById(
            GetAccountInfoByIdQuery query,
            CancellationToken cancellationToken
        )
    {
        var result = await _mediatr.Send(query, cancellationToken);
        return result.Handle(Ok, e => e.ToProblemDetails());
    }

    [HttpGet] [Authorize]
    public async Task<ActionResult<AccountInfo>> GetById(GetAccountByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediatr.Send(query, cancellationToken);
        return result.Handle(Ok, e => e.ToProblemDetails());
    }
}
