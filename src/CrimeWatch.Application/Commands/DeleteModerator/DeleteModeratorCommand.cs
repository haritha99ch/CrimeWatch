namespace CrimeWatch.Application.Commands.DeleteModerator;
public sealed record DeleteModeratorCommand(ModeratorId ModeratorId) : IRequest<bool>;
