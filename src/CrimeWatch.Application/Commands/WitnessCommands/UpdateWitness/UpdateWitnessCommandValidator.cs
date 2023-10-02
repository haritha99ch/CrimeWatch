namespace CrimeWatch.Application.Commands.WitnessCommands.UpdateWitness;
public class UpdateWitnessCommandValidator : HttpContextValidator<UpdateWitnessCommand>
{
    public UpdateWitnessCommandValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(e => e.Id)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to update this witness.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private Task<bool> HasPermissions(WitnessId witnessId, CancellationToken cancellationToken)
        => Task.FromResult(UserClaims.WitnessId != null && UserClaims.WitnessId.Equals(witnessId));
}
