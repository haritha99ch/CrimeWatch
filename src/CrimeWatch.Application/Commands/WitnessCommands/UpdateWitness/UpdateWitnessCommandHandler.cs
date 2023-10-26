namespace CrimeWatch.Application.Commands.WitnessCommands.UpdateWitness;
internal class UpdateWitnessCommandHandler(IRepository<Witness, WitnessId> witnessRepository)
    : IRequestHandler<UpdateWitnessCommand, Witness>
{

    public async Task<Witness> Handle(UpdateWitnessCommand request, CancellationToken cancellationToken)
    {
        var witness = await witnessRepository.GetWitnessWithAllByIdAsync(request.Id, cancellationToken)
            ?? throw new("Witness not found");

        witness.Update(
            request.FirstName,
            request.LastName,
            request.Gender,
            request.DateOfBirth,
            request.Email,
            request.Password,
            request.PhoneNumber
        );

        return await witnessRepository.UpdateAsync(witness, cancellationToken);
    }
}
