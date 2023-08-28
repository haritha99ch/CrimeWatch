using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Commands.CreateModerator;
internal class CreateModeratorCommandHandler : IRequestHandler<CreateModeratorCommand, Moderator>
{
    private readonly IRepository<Moderator, ModeratorId> _moderatorRepository;

    public CreateModeratorCommandHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    {
        _moderatorRepository = moderatorRepository;
    }

    public async Task<Moderator> Handle(CreateModeratorCommand request, CancellationToken cancellationToken)
    {
        Moderator moderator = Moderator
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

        return await _moderatorRepository.AddAsync(moderator, cancellationToken);
    }
}
