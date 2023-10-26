namespace CrimeWatch.Application.Commands.ModeratorCommands.UpdateModerator;
internal class UpdateModeratorCommandHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    : IRequestHandler<UpdateModeratorCommand, Moderator>
{

    public async Task<Moderator> Handle(UpdateModeratorCommand request, CancellationToken cancellationToken)
    {
        var moderator =
            await moderatorRepository.GetModeratorWithAllByIdAsync(request.Id, cancellationToken)
            ?? throw new("Moderator not found");

        moderator.Update(
            request.FirstName,
            request.LastName,
            request.Gender,
            request.DateOfBirth,
            request.PhoneNumber,
            request.PoliceId,
            request.Province,
            request.Email,
            request.Password
        );

        return await moderatorRepository.UpdateAsync(moderator, cancellationToken);
    }
}
