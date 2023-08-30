using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Application.Queries.GetAccount;
public sealed record GetAccountBySignInQuery
    (string Email, string Password) : IRequest<Account>;
