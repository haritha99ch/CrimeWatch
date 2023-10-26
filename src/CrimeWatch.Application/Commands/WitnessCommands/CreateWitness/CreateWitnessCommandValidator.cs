namespace CrimeWatch.Application.Commands.WitnessCommands.CreateWitness;
public sealed class CreateWitnessCommandValidator : AbstractValidator<CreateWitnessCommand>
{
    public CreateWitnessCommandValidator(IRepository<Account, AccountId> accountRepository)
    {
        RuleFor(e => e.Email)
            .MustAsync(accountRepository.IsEmailUniqueAsync)
            .WithMessage("Email already exists")
            .WithErrorCode(StatusCodes.Status409Conflict.ToString());
    }
}
