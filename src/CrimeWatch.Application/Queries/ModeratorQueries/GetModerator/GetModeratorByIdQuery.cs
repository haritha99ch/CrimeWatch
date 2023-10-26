namespace CrimeWatch.Application.Queries.ModeratorQueries.GetModerator;
public sealed record GetModeratorByIdQuery(ModeratorId ModeratorId) : IRequest<Moderator>;
