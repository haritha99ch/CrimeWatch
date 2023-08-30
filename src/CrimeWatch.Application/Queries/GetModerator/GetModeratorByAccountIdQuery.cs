using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Queries.GetModerator;
public sealed record GetModeratorByAccountIdQuery(AccountId AccountId) : IRequest<Moderator>;
