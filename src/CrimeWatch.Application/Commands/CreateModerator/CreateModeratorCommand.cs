using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Commands.CreateModerator;
public sealed record
    CreateModeratorCommand(
        string FirstName,
        string LastName,
        Gender Gender,
        DateOnly DateOfBirth,
        string PhoneNumber,
        string PoliceId,
        string Province,
        string Email,
        string Password
    ) : IRequest<Moderator>;
