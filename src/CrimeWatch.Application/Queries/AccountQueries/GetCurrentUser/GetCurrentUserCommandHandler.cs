using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Application.Queries.ModeratorQueries.GetModerator;
using CrimeWatch.Application.Queries.WitnessQueries.GetWitness;
using CrimeWatch.Application.Shared;

namespace CrimeWatch.Application.Queries.AccountQueries.GetCurrentUser;
internal class GetCurrentUserCommandHandler
    : IRequestHandler<GetCurrentUserCommand, ModeratorOrWitness>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMediator _mediator;


    public GetCurrentUserCommandHandler(IAuthenticationService authenticationService, IMediator mediator)
    {
        _mediator = mediator;
        _authenticationService = authenticationService;
    }

    public async Task<ModeratorOrWitness> Handle(GetCurrentUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = _authenticationService.Authenticate();
        return await result.Authorize<Task<ModeratorOrWitness>>(
            async m => await _mediator.Send(new GetModeratorByIdQuery(m), cancellationToken),
            async w => await _mediator.Send(new GetWitnessByIdQuery(w), cancellationToken),
            _ => Task.FromResult<ModeratorOrWitness>("Not Found"));
    }
}
