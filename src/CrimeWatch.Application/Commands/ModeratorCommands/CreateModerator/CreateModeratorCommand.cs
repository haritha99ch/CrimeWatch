using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Commands.ModeratorCommands.CreateModerator;
public sealed record
    CreateModeratorCommand(
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
