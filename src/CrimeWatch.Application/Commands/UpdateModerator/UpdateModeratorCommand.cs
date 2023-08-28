using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Commands.UpdateModerator;
public sealed record
    UpdateModeratorCommand(
        ModeratorId Id,
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
