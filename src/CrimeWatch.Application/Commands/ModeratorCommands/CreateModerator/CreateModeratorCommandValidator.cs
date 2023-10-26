namespace CrimeWatch.Application.Commands.ModeratorCommands.CreateModerator;
public sealed class CreateModeratorCommandValidator : AbstractValidator<CreateModeratorCommand>
{
    public CreateModeratorCommandValidator(
        IRepository<Moderator, ModeratorId> moderatorRepository,
        IRepository<Account, AccountId> accountRepository)
    {
        RuleFor(e => e.Email)
            .MustAsync(accountRepository.IsEmailUniqueAsync)
            .WithMessage("Email is already taken.")
            .WithErrorCode(StatusCodes.Status409Conflict.ToString());

        RuleFor(e => e.PoliceId)
            .MustAsync(moderatorRepository.IsPoliceIdUniqueAsync)
            .WithMessage("Police Id is already taken.")
            .WithErrorCode(StatusCodes.Status409Conflict.ToString());
    }
}
