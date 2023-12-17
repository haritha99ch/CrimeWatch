namespace Application.Features.Accounts.Queries.SignInToAccount;
public sealed record SignInToAccountQuery(string Email, string Password) : IQuery<string>;
