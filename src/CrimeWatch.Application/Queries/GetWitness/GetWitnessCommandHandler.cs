using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Queries.GetWitness;
internal class GetWitnessCommandHandler : IRequestHandler<GetWitnessCommand, Witness>
{
    private readonly IRepository<Witness, WitnessId> _witnessRepository;

    public GetWitnessCommandHandler(IRepository<Witness, WitnessId> witnessRepository)
    {
        _witnessRepository = witnessRepository;
    }

    public async Task<Witness> Handle(GetWitnessCommand request, CancellationToken cancellationToken)
        => await _witnessRepository.GetByAsync<WitnessByIdIncludingAll>(new(request.WitnessId), cancellationToken)
        ?? throw new Exception("Witness not found");
}
