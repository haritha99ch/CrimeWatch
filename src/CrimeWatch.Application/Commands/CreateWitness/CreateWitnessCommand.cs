using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Commands.CreateWitness;
public sealed record
    CreateWitnessCommand(
        string FirstName,
        string LastName,
        Gender Gender,
        DateOnly DateOfBirth,
        string PhoneNumber,
        string Email,
        string Password
        ) : IRequest<Witness>;
