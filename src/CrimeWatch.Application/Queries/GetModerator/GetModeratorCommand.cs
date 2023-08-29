using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Queries.GetModerator;
public sealed record GetModeratorCommand(ModeratorId ModeratorId) : IRequest<Moderator>;
