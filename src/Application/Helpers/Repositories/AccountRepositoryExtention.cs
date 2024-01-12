using Application.Specifications.AccountSpecifications;

namespace Application.Helpers.Repositories;
internal static class AccountRepositoryExtension
{
    internal async static Task<Account?> GetAccountByEmail(
            this IRepository<Account, AccountId> repository,
            string email,
            CancellationToken cancellationToken
        )
    {
        var account = await repository.GetOneAsync(new AccountByEmail(email));
        return account;
    }

    internal async static Task<Account?> GetModeratorAccountIncludingOwnedById(
            this IRepository<Account, AccountId> repository,
            AccountId accountId,
            CancellationToken cancellationToken
        ) => await repository.GetOneAsync<ModeratorAccountIncludingOwned>(
        new(accountId),
        cancellationToken);

    internal async static Task<Account?> GetWitnessAccountIncludingOwnedById(
            this IRepository<Account, AccountId> repository,
            AccountId accountId,
            CancellationToken cancellationToken
        ) => await repository.GetOneAsync<WitnessAccountIncludingOwned>(
        new(accountId),
        cancellationToken);
}
