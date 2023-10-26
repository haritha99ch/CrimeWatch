namespace CrimeWatch.Application.Queries.WitnessQueries.GetWitness;
public sealed record GetWitnessByAccountIdQuery(AccountId AccountId) : IRequest<Witness>;
