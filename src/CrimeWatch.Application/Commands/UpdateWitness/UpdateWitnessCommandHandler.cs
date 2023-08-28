using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Commands.UpdateWitness;
internal class UpdateWitnessCommandHandler : IRequestHandler<UpdateWitnessCommand, Witness>
{
    private readonly IRepository<Witness, WitnessId> _witnessRepository;

    public UpdateWitnessCommandHandler(IRepository<Witness, WitnessId> witnessRepository)
    {
        _witnessRepository = witnessRepository;
    }

    public async Task<Witness> Handle(UpdateWitnessCommand request, CancellationToken cancellationToken)
    {
        GetWitnessByIdIncludingAll specification = new(request.Id);

        Witness witness = await _witnessRepository.GetByAsync(specification, cancellationToken)
            ?? throw new Exception("Witness not found");

        witness.User!.FirstName = request.FirstName;
        witness.User!.LastName = request.LastName;
        witness.User!.Gender = request.Gender;
        witness.User!.DateOfBirth = request.DateOfBirth;
        witness.User!.PhoneNumber = request.PhoneNumber;
        witness.Account!.Email = request.Email;
        witness.Account!.Password = request.Password;

        return await _witnessRepository.UpdateAsync(witness, cancellationToken);
    }
}
