namespace CrimeWatch.Application.Commands.ModeratorCommands.UpdateModerator;
public class UpdateModeratorCommandValidator : HttpContextValidator<UpdateModeratorCommand>
{
    public UpdateModeratorCommandValidator(IHttpContextAccessor contextAccessor) : base(contextAccessor)
    {
        RuleFor(x => x.Id)
            .MustAsync(HasPermission)
            .WithMessage("You do not have permission to update this moderator.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private Task<bool> HasPermission(ModeratorId moderatorId, CancellationToken cancellationToken)
        => Task.FromResult(UserClaims.ModeratorId != null && UserClaims.ModeratorId.Equals(moderatorId));
}
