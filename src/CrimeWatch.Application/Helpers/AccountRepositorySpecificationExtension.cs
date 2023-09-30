using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Application.Helpers;
internal static class AccountRepositorySpecificationExtension
{
    public static async Task<Account?> GetAccountBySignInAsync(this IRepository<Account, AccountId> repository, string email, string password, CancellationToken cancellationToken)
        => await repository.GetByAsync<AccountBySignIn>(new(email, password), cancellationToken);
}
