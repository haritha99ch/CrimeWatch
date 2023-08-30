using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Queries.GetWitness;
public sealed record GetWitnessByAccountIdQuery(AccountId AccountId) : IRequest<Witness>;
