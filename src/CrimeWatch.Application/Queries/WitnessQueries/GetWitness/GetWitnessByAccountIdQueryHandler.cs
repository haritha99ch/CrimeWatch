using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Queries.WitnessQueries.GetWitness;
internal class GetWitnessByAccountIdQueryHandler : IRequestHandler<GetWitnessByAccountIdQuery, Witness>
{
    private readonly IRepository<Witness, WitnessId> _witnessRepository;

    public GetWitnessByAccountIdQueryHandler(IRepository<Witness, WitnessId> witnessRepository)
    {
        _witnessRepository = witnessRepository;
    }
    public async Task<Witness> Handle(GetWitnessByAccountIdQuery request, CancellationToken cancellationToken)
        => await _witnessRepository.GetWitnessWithAllByAccountIdAsync(request.AccountId, cancellationToken)
        ?? throw new Exception("Witness not found");
}
