using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Queries.GetWitness;
internal class GetWitnessQueryHandler : IRequestHandler<GetWitnessQuery, Witness>
{
    private readonly IRepository<Witness, WitnessId> _witnessRepository;

    public GetWitnessQueryHandler(IRepository<Witness, WitnessId> witnessRepository)
    {
        _witnessRepository = witnessRepository;
    }

    public async Task<Witness> Handle(GetWitnessQuery request, CancellationToken cancellationToken)
        => await _witnessRepository.GetWitnessWithAllByIdAsync(request.WitnessId, cancellationToken)
        ?? throw new Exception("Witness not found");
}
