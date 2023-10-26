namespace CrimeWatch.Application.Commands.WitnessCommands.CreateWitness;
public sealed record CreateWitnessCommand(
        string FirstName,
        string LastName,
        Gender Gender,
        DateTime DateOfBirth,
        string PhoneNumber,
        string Email,
        string Password
    ) : IRequest<Witness>;
