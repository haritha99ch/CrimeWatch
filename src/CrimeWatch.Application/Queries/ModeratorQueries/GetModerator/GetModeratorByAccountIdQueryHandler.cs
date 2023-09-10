using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Queries.ModeratorQueries.GetModerator;
internal class GetModeratorByAccountIdQueryHandler : IRequestHandler<GetModeratorByAccountIdQuery, Moderator>
{
    private readonly IRepository<Moderator, ModeratorId> _moderatorRepository;

    public GetModeratorByAccountIdQueryHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    {
        _moderatorRepository = moderatorRepository;
    }

    public async Task<Moderator> Handle(GetModeratorByAccountIdQuery request, CancellationToken cancellationToken)
        => await _moderatorRepository.GetModeratorWithAllByAccountIdAsync(request.AccountId, cancellationToken)
        ?? throw new Exception($"Moderator not found");
}
