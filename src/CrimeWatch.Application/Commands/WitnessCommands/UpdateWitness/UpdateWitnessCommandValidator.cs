using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.WitnessCommands.UpdateWitness;
public class UpdateWitnessCommandValidator : HttpContextValidator<UpdateWitnessCommand>
{
    public UpdateWitnessCommandValidator(IAuthenticationService authenticationService) : base(authenticationService)
    {
        RuleFor(e => e.Id)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to update this witness.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(WitnessId witnessId)
        => _authenticationService.Authenticate()
            .Authorize(witnessId: witnessId.Equals);
}
