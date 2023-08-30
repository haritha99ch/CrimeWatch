using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Queries.GetModerator;
internal class GetModeratorQueryHandler : IRequestHandler<GetModeratorQuery, Moderator>
{
    private readonly IRepository<Moderator, ModeratorId> _moderatorRepository;

    public GetModeratorQueryHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    {
        _moderatorRepository = moderatorRepository;
    }

    public async Task<Moderator> Handle(GetModeratorQuery request, CancellationToken cancellationToken)
        => await _moderatorRepository.GetModeratorWithAllByIdAsync(request.ModeratorId, cancellationToken)
        ?? throw new Exception($"Moderator not found");
}
