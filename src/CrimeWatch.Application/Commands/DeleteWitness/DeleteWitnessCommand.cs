namespace CrimeWatch.Application.Commands.DeleteWitness;
public sealed record DeleteWitnessCommand(WitnessId WitnessId) : IRequest<bool>;
