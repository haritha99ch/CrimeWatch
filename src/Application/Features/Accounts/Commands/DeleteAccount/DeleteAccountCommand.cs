namespace Application.Features.Accounts.Commands.DeleteAccount;

public sealed record DeleteAccountCommand(AccountId AccountId) : ICommand<bool>;
