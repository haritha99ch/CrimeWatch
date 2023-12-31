﻿using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Commands.WitnessCommands.UpdateWitness;
internal class UpdateWitnessCommandHandler : IRequestHandler<UpdateWitnessCommand, Witness>
{
    private readonly IRepository<Witness, WitnessId> _witnessRepository;

    public UpdateWitnessCommandHandler(IRepository<Witness, WitnessId> witnessRepository)
    {
        _witnessRepository = witnessRepository;
    }

    public async Task<Witness> Handle(UpdateWitnessCommand request, CancellationToken cancellationToken)
    {
        var witness = await _witnessRepository.GetWitnessWithAllByIdAsync(request.Id, cancellationToken)
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

        return await _witnessRepository.UpdateAsync(witness, cancellationToken);
    }
}
