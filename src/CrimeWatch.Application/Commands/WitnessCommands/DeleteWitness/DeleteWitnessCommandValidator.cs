using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.WitnessCommands.DeleteWitness;
public class DeleteWitnessCommandValidator : HttpContextValidator<DeleteWitnessCommand>
{
    public DeleteWitnessCommandValidator(IAuthenticationService authenticationService) : base(authenticationService)
    {
        RuleFor(e => e.WitnessId)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to delete this witness.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(WitnessId witnessId)
        => _authenticationService.Authenticate()
            .Authorize(witnessId: witnessId.Equals);
}
