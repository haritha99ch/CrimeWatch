namespace CrimeWatch.Application.Queries.ModeratorQueries.GetModerator;
internal class GetModeratorQueryByIdHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    : IRequestHandler<GetModeratorByIdQuery, Moderator>
{

    public async Task<Moderator> Handle(GetModeratorByIdQuery request, CancellationToken cancellationToken)
        => await moderatorRepository.GetModeratorWithAllByIdAsync(request.ModeratorId, cancellationToken)
            ?? throw new("Moderator not found");
}
