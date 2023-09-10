namespace CrimeWatch.Application.Commands.WitnessCommands.DeleteWitness;
public sealed record DeleteWitnessCommand(WitnessId WitnessId) : IRequest<bool>;
