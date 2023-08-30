﻿using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Commands.UpdateModerator;
internal class UpdateModeratorCommandHandler : IRequestHandler<UpdateModeratorCommand, Moderator>
{
    private readonly IRepository<Moderator, ModeratorId> _moderatorRepository;

    public UpdateModeratorCommandHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    {
        _moderatorRepository = moderatorRepository;
    }

    public async Task<Moderator> Handle(UpdateModeratorCommand request, CancellationToken cancellationToken)
    {
        Moderator? moderator =
            await _moderatorRepository.GetModeratorWithAllByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception("Moderator not found");

        moderator.Update(
            request.FirstName,
            request.LastName,
            request.Gender,
            request.DateOfBirth,
            request.Email,
            request.Password,
            request.PhoneNumber,
            request.PoliceId,
            request.Province
            );

        return await _moderatorRepository.UpdateAsync(moderator, cancellationToken);
    }
}
