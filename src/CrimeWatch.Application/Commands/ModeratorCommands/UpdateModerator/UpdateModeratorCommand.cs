using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Commands.ModeratorCommands.UpdateModerator;
public sealed record UpdateModeratorCommand(
        ModeratorId Id,
        string FirstName,
        string LastName,
        Gender Gender,
        DateTime DateOfBirth,
        string PhoneNumber,
        string PoliceId,
        string Province,
        string Email,
        string Password
    ) : IRequest<Moderator>;
