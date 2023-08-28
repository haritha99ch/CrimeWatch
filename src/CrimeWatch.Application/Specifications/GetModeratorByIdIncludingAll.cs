using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;

namespace CrimeWatch.Application.Specifications;
internal class GetModeratorByIdIncludingAll : Specification<Moderator, ModeratorId>
{
    public GetModeratorByIdIncludingAll(ModeratorId moderator) : base(e => e.Id.Equals(moderator))
    {
        AddInclude(e => e.Include(e => e.User));
        AddInclude(e => e.Include(e => e.Account));
    }
}