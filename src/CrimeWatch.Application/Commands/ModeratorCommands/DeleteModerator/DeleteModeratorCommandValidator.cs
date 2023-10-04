namespace CrimeWatch.Application.Commands.ModeratorCommands.DeleteModerator;
public class DeleteModeratorCommandValidator : HttpContextValidator<DeleteModeratorCommand>
{
    public DeleteModeratorCommandValidator(IHttpContextAccessor contextAccessor) : base(contextAccessor)
    {
        RuleFor(x => x.ModeratorId)
            .Must(HasPermission)
            .WithMessage("You do not have permission to update this moderator.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermission(ModeratorId moderatorId) => moderatorId.Equals(UserClaims.ModeratorId);
}
