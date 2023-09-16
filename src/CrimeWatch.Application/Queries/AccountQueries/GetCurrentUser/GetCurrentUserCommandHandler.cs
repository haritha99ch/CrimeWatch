using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Application.Queries.ModeratorQueries.GetModerator;
using CrimeWatch.Application.Queries.WitnessQueries.GetWitness;

namespace CrimeWatch.Application.Queries.AccountQueries.GetCurrentUser;
internal class GetCurrentUserCommandHandler : IRequestHandler<GetCurrentUserCommand, object>
{
    private readonly IMediator _mediator;
    private readonly IAuthenticationService _authenticationService;

    public GetCurrentUserCommandHandler(IAuthenticationService authenticationService, IMediator mediator)
    {
        _mediator = mediator;
        _authenticationService = authenticationService;
    }

    public async Task<object> Handle(GetCurrentUserCommand request, CancellationToken cancellationToken)
    {
        var result = _authenticationService.Authenticate();
        if (result.Item1) return await _mediator.Send(new GetModeratorByIdQuery(new(new(result.Item2!))), cancellationToken);
        return await _mediator.Send(new GetWitnessByIdQuery(new(new(result.Item2!))), cancellationToken);
    }
}
