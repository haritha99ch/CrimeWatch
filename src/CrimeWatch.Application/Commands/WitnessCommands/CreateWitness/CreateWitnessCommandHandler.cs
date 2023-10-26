namespace CrimeWatch.Application.Commands.WitnessCommands.CreateWitness;
internal class CreateWitnessCommandHandler : IRequestHandler<CreateWitnessCommand, Witness>
{
    private readonly IRepository<Witness, WitnessId> _witnessRepository;

    public CreateWitnessCommandHandler(IRepository<Witness, WitnessId> witnessRepository)
    {
        _witnessRepository = witnessRepository;
    }

    public async Task<Witness> Handle(CreateWitnessCommand request, CancellationToken cancellationToken)
    {
        var witness = Witness
            .Create(request.FirstName, request.LastName, request.Gender, request.DateOfBirth,
                request.PhoneNumber, request.Email, request.Password);

        return await _witnessRepository.AddAsync(witness, cancellationToken);
    }
}
