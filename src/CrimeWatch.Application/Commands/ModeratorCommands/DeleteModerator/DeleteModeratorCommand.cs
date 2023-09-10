namespace CrimeWatch.Application.Commands.ModeratorCommands.DeleteModerator;
public sealed record DeleteModeratorCommand(ModeratorId ModeratorId) : IRequest<bool>;
