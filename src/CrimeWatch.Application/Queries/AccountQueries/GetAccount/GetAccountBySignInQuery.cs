namespace CrimeWatch.Application.Queries.AccountQueries.GetAccount;
public sealed record GetAccountBySignInQuery(string Email, string Password) : IRequest<string>;
