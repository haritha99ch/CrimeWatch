using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Permissions.Commands.WitnessCommands;
internal class EditWitnessPermissionHandler : RequestPermissions,
    IRequestHandler<EditWitnessPermission, UserPermissions>
{
    private readonly IRepository<Witness, WitnessId> _witnessRepository;

    public EditWitnessPermissionHandler(
            IHttpContextAccessor httpContextAccessor,
            IRepository<Witness, WitnessId> witnessRepository
        ) : base(httpContextAccessor)
    {
        _witnessRepository = witnessRepository;
    }

    public async Task<UserPermissions> Handle(EditWitnessPermission request, CancellationToken cancellationToken)
    {
        if (!UserClaims.UserType.Equals(UserType.Witness)) return UserPermissions.ViewOnly;

        var witness = await _witnessRepository.GetByIdAsync(request.WitnessId, e => new { e.Id }, cancellationToken);
        if (witness is null) return UserPermissions.ViewOnly;

        return witness.Id.Equals(UserClaims.WitnessId) ? UserPermissions.FullAccess : UserPermissions.ViewOnly;
    }
}
