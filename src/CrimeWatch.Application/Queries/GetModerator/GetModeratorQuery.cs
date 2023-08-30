using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Queries.GetModerator;
public sealed record GetModeratorQuery(ModeratorId ModeratorId) : IRequest<Moderator>;
