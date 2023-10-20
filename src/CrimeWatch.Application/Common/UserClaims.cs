using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;
using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Common;
public sealed record UserClaims(string? UserId, string UserRole = "")
{
    public UserType UserType => UserRole switch
    {
        nameof(Moderator) => UserType.Moderator,
        nameof(Witness) => UserType.Witness,
        _ => UserType.Guest
    };
    public WitnessId? WitnessId => !string.IsNullOrEmpty(UserId) && UserType.Equals(UserType.Witness)
        ? new(new(UserId))
        : throw new("Not a Witness");
    public ModeratorId? ModeratorId => !string.IsNullOrEmpty(UserId) && UserType.Equals(UserType.Moderator)
        ? new(new(UserId))
        : throw new("Not a Moderator");
}
