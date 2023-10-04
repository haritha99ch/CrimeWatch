namespace CrimeWatch.Application.Commands.WitnessCommands.UpdateWitness;
public class UpdateWitnessCommandValidator : HttpContextValidator<UpdateWitnessCommand>
{
    public UpdateWitnessCommandValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(e => e.Id)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to update this witness.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(WitnessId witnessId) => witnessId.Equals(UserClaims.WitnessId);
}
