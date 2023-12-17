using Domain.AggregateModels.AccountAggregate;
using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;

namespace Persistence.Test.Repositories.AccountRepository.TestSpecifications;
internal record AccountByTestSpecification : Specification<Account>
{
    internal AccountByTestSpecification(string email)
        : base(a => a.Email.Equals(email))
    {
        AddInclude(q => q.Include(a => a.Moderator!));
        AddInclude(q => q.Include(a => a.Person!));
    }
}
