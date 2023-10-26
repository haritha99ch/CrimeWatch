namespace CrimeWatch.Application.Queries.ModeratorQueries.GetModerator;
internal class GetModeratorByAccountIdQueryHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    : IRequestHandler<GetModeratorByAccountIdQuery, Moderator>
{

    public async Task<Moderator> Handle(GetModeratorByAccountIdQuery request, CancellationToken cancellationToken)
        => await moderatorRepository.GetModeratorWithAllByAccountIdAsync(request.AccountId, cancellationToken)
            ?? throw new("Moderator not found");
}
