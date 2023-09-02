using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Commands.UpdateWitness;
public sealed record
    UpdateWitnessCommand(
        WitnessId Id,
        string FirstName,
        string LastName,
        Gender Gender,
        DateTime DateOfBirth,
        string PhoneNumber,
        string Email,
        string Password
        ) : IRequest<Witness>;
