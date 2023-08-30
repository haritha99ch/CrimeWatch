using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Queries.GetWitness;
public sealed record GetWitnessQuery(WitnessId WitnessId) : IRequest<Witness>;
