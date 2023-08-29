using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Queries.GetWitness;
public sealed record GetWitnessCommand(WitnessId WitnessId) : IRequest<Witness>;
