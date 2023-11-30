using Application.Specifications.AccountSpecifications;

namespace Application.Helpers.Repositories;

internal static class AccountRepositoryExtensions
{
    internal static async Task<Account?> GetAccountByEmail(
        this IRepository<Account, AccountId> repository,
        string email,
        CancellationToken cancellationToken
    )
    {
        var account = await repository.GetOneAsync(new AccountByEmail(email));
        return account;
    }

    internal static async Task<Account?> GetModeratorAccountIncludingOwnedById(
        this IRepository<Account, AccountId> repository,
        AccountId accountId,
        CancellationToken cancellationToken
    )
    {
        return await repository.GetOneAsync<ModeratorAccountIncludingOwned>(
            new(accountId),
            cancellationToken
        );
    }
}
