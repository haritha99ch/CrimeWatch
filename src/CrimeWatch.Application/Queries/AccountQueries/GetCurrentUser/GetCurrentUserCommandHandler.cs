using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Application.Queries.ModeratorQueries.GetModerator;
using CrimeWatch.Application.Queries.WitnessQueries.GetWitness;
using CrimeWatch.Application.Shared;

namespace CrimeWatch.Application.Queries.AccountQueries.GetCurrentUser;
internal class GetCurrentUserCommandHandler(
        IAuthenticationService authenticationService,
        ISender mediator)
    : IRequestHandler<GetCurrentUserCommand, ModeratorOrWitness>
{


    public async Task<ModeratorOrWitness> Handle(GetCurrentUserCommand request,
        CancellationToken cancellationToken)
    {
        var result = authenticationService.Authenticate();
        return await result.Authorize<Task<ModeratorOrWitness>>(
            async m => await mediator.Send(new GetModeratorByIdQuery(m), cancellationToken),
            async w => await mediator.Send(new GetWitnessByIdQuery(w), cancellationToken),
            _ => Task.FromResult<ModeratorOrWitness>("Not Found"));
    }
}
