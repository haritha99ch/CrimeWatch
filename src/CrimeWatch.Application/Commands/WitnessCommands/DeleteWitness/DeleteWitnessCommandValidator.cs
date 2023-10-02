namespace CrimeWatch.Application.Commands.WitnessCommands.DeleteWitness;
public class DeleteWitnessCommandValidator : HttpContextValidator<DeleteWitnessCommand>
{
    public DeleteWitnessCommandValidator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        RuleFor(e => e.WitnessId)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to delete this witness.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private Task<bool> HasPermissions(WitnessId witnessId, CancellationToken cancellationToken)
        => Task.FromResult(UserClaims.WitnessId != null && UserClaims.WitnessId.Equals(witnessId));
}
