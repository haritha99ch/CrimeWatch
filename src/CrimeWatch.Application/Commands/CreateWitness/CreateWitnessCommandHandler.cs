using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Commands.CreateWitness;
internal class CreateWitnessCommandHandler : IRequestHandler<CreateWitnessCommand, Witness>
{
    private readonly IRepository<Witness, WitnessId> witnessRepository;

    public CreateWitnessCommandHandler(IRepository<Witness, WitnessId> witnessRepository)
    {
        this.witnessRepository = witnessRepository;
    }

    public async Task<Witness> Handle(CreateWitnessCommand request, CancellationToken cancellationToken)
    {
        Witness witness = Witness
            .Create(request.FirstName, request.LastName, request.Gender, request.DateOfBirth,
                request.PhoneNumber, request.Email, request.Password);

        return await witnessRepository.AddAsync(witness, cancellationToken);
    }
}
