using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Permissions.Commands.ModeratorCommands;
internal class EditModeratorPermissionHandler : RequestPermissions,
    IRequestHandler<EditModeratorPermission, UserPermissions>
{
    private readonly IRepository<Moderator, ModeratorId> _moderatorRepository;

    public EditModeratorPermissionHandler(
            IHttpContextAccessor httpContextAccessor,
            IRepository<Moderator, ModeratorId> moderatorRepository
        ) : base(httpContextAccessor)
    {
        _moderatorRepository = moderatorRepository;
    }

    public async Task<UserPermissions> Handle(EditModeratorPermission request, CancellationToken cancellationToken)
    {
        if (!UserClaims.UserType.Equals(UserType.Moderator)) return UserPermissions.ViewOnly;

        var moderator = await _moderatorRepository.GetByIdAsync(
            request.ModeratorId,
            e => new { e.Id },
            cancellationToken);
        if (moderator == null) return UserPermissions.ViewOnly;
        
        return moderator.Id.Equals(UserClaims.ModeratorId) ? UserPermissions.FullAccess : UserPermissions.ViewOnly;
    }
}
