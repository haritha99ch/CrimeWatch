using CrimeWatch.Application.Shared;

namespace CrimeWatch.Application.Queries.AccountQueries.GetCurrentUser;
public sealed record GetCurrentUserCommand : IRequest<ModeratorOrWitness>;
