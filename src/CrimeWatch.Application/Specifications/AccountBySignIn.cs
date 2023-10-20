using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Application.Specifications;
internal class AccountBySignIn : Specification<Account, AccountId>
{
    public AccountBySignIn(string email, string password)
        : base(e => e.Email.Equals(email) && e.Password.Equals(password)) { }
}
