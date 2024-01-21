namespace Application.Features.Accounts.Queries.GetAccountInfoById;
public sealed record GetAccountInfoByIdQuery(AccountId AccountId) : IQuery<AccountInfo>;
