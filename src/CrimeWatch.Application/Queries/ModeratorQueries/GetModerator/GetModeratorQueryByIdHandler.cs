using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Queries.ModeratorQueries.GetModerator;
internal class GetModeratorQueryByIdHandler : IRequestHandler<GetModeratorByIdQuery, Moderator>
{
    private readonly IRepository<Moderator, ModeratorId> _moderatorRepository;

    public GetModeratorQueryByIdHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    {
        _moderatorRepository = moderatorRepository;
    }

    public async Task<Moderator> Handle(GetModeratorByIdQuery request, CancellationToken cancellationToken)
        => await _moderatorRepository.GetModeratorWithAllByIdAsync(request.ModeratorId, cancellationToken)
            ?? throw new("Moderator not found");
}
