using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Queries.GetModerator;
internal class GetModeratorCommandHandler : IRequestHandler<GetModeratorCommand, Moderator>
{
    private readonly IRepository<Moderator, ModeratorId> _moderatorRepository;

    public GetModeratorCommandHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    {
        _moderatorRepository = moderatorRepository;
    }

    public async Task<Moderator> Handle(GetModeratorCommand request, CancellationToken cancellationToken)
        => await _moderatorRepository.GetModeratorWithAllByIdAsync(request.ModeratorId, cancellationToken)
        ?? throw new Exception($"Moderator not found");
}
