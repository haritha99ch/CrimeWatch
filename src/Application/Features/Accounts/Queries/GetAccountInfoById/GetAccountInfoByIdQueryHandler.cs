﻿using Application.Specifications.Accounts;

namespace Application.Features.Accounts.Queries.GetAccountInfoById;
internal sealed class GetAccountInfoByIdQueryHandler
    : IQueryHandler<GetAccountInfoByIdQuery, AccountInfo>
{
    private readonly IRepository<Account, AccountId> _accountRepository;

    public GetAccountInfoByIdQueryHandler(IRepository<Account, AccountId> accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Result<AccountInfo>> Handle(
            GetAccountInfoByIdQuery request,
            CancellationToken cancellationToken
        )
    {
        var accountInfo = await _accountRepository.GetOneAsync<AccountInfoById, AccountInfo>(
            new(request.AccountId),
            cancellationToken);
        return accountInfo is not null ? accountInfo : AccountNotFoundError.Create();
    }
}
