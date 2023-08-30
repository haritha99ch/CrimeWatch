using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Queries.GetModerator;
public sealed record GetModeratorByIdQuery(ModeratorId ModeratorId) : IRequest<Moderator>;
