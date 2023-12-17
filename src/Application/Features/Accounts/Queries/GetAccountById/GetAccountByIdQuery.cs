namespace Application.Features.Accounts.Queries.GetAccountById;
public sealed record GetAccountByIdQuery(AccountId AccountId) : IQuery<Account>;
