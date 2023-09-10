using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Application.Queries.AccountQueries.GetAccount;
public sealed record GetAccountBySignInQuery
    (string Email, string Password) : IRequest<Account>;
