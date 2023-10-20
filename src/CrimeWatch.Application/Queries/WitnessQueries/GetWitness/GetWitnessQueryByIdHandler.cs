using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Queries.WitnessQueries.GetWitness;
internal class GetWitnessQueryByIdHandler : IRequestHandler<GetWitnessByIdQuery, Witness>
{
    private readonly IRepository<Witness, WitnessId> _witnessRepository;

    public GetWitnessQueryByIdHandler(IRepository<Witness, WitnessId> witnessRepository)
    {
        _witnessRepository = witnessRepository;
    }

    public async Task<Witness> Handle(GetWitnessByIdQuery request, CancellationToken cancellationToken)
        => await _witnessRepository.GetWitnessWithAllByIdAsync(request.WitnessId, cancellationToken)
            ?? throw new("Witness not found");
}
