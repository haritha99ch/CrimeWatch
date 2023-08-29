using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Application.Queries.GetAccount;
public sealed record GetAccountBySignInCommand
    (string Email, string Password) : IRequest<Account>;
