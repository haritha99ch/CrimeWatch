using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Commands.DeleteWitness;
internal class DeleteWitnessCommandHandler : IRequestHandler<DeleteWitnessCommand, bool>
{
    private readonly IRepository<Witness, WitnessId> _witnessRepository;

    public DeleteWitnessCommandHandler(IRepository<Witness, WitnessId> witnessRepository)
    {
        _witnessRepository = witnessRepository;
    }

    public async Task<bool> Handle(DeleteWitnessCommand request, CancellationToken cancellationToken)
        => await _witnessRepository.DeleteByIdAsync(request.WitnessId, cancellationToken);
}
