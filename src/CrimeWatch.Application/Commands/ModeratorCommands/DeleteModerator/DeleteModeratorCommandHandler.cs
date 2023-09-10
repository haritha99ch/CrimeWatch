using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Commands.ModeratorCommands.DeleteModerator;
internal class DeleteModeratorCommandHandler : IRequestHandler<DeleteModeratorCommand, bool>
{
    private readonly IRepository<Moderator, ModeratorId> _moderatorRepository;

    public DeleteModeratorCommandHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    {
        _moderatorRepository = moderatorRepository;
    }

    public async Task<bool> Handle(DeleteModeratorCommand request, CancellationToken cancellationToken)
        => await _moderatorRepository.DeleteByIdAsync(request.ModeratorId, cancellationToken);
}
