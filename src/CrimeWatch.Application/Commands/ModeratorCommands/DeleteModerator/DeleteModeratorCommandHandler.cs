namespace CrimeWatch.Application.Commands.ModeratorCommands.DeleteModerator;
internal class DeleteModeratorCommandHandler(IRepository<Moderator, ModeratorId> moderatorRepository)
    : IRequestHandler<DeleteModeratorCommand, bool>
{

    public async Task<bool> Handle(DeleteModeratorCommand request, CancellationToken cancellationToken)
        => await moderatorRepository.DeleteByIdAsync(request.ModeratorId, cancellationToken);
}
