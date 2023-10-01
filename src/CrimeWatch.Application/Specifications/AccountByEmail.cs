using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Application.Specifications;
public class AccountByEmail : Specification<Account, AccountId>
{
    public AccountByEmail(string email) : base(e => e.Email == email) { }
}
