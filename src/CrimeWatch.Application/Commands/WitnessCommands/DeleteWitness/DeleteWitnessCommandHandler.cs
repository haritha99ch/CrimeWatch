namespace CrimeWatch.Application.Commands.WitnessCommands.DeleteWitness;
internal class DeleteWitnessCommandHandler(IRepository<Witness, WitnessId> witnessRepository)
    : IRequestHandler<DeleteWitnessCommand, bool>
{

    public async Task<bool> Handle(DeleteWitnessCommand request, CancellationToken cancellationToken)
        => await witnessRepository.DeleteByIdAsync(request.WitnessId, cancellationToken);
}
