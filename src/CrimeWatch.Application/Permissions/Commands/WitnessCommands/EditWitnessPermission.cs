namespace CrimeWatch.Application.Permissions.Commands.WitnessCommands;
public sealed record EditWitnessPermission(WitnessId WitnessId) : IRequest<UserPermissions>;
