namespace CrimeWatch.Application.Commands.WitnessCommands.DeleteWitness;
public class DeleteWitnessCommandValidator : HttpContextValidator<DeleteWitnessCommand>
{
    public DeleteWitnessCommandValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(e => e.WitnessId)
            .Must(HasPermissions)
            .WithMessage("You do not have permission to delete this witness.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermissions(WitnessId witnessId) => witnessId.Equals(UserClaims.WitnessId);
}
