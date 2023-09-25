namespace CrimeWatch.Application.Permissions.Commands.ModeratorCommands;
public sealed record EditModeratorPermission(ModeratorId ModeratorId) : IRequest<UserPermissions>;
