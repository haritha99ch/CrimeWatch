namespace CrimeWatch.Application.Commands.ModeratorCommands.CreateModerator;
internal class CreateModeratorCommandHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    : IRequestHandler<CreateModeratorCommand, Moderator>
{

    public async Task<Moderator> Handle(CreateModeratorCommand request, CancellationToken cancellationToken)
    {
        var moderator = Moderator
            .Create(
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

        return await moderatorRepository.AddAsync(moderator!, cancellationToken);
    }
}
