namespace CrimeWatch.Application.Commands.ModeratorCommands.UpdateModerator;
public class UpdateModeratorCommandValidator : HttpContextValidator<UpdateModeratorCommand>
{
    public UpdateModeratorCommandValidator(IHttpContextAccessor contextAccessor) : base(contextAccessor)
    {
        RuleFor(x => x.Id)
            .Must(HasPermission)
            .WithMessage("You do not have permission to update this moderator.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermission(ModeratorId moderatorId) => moderatorId.Equals(UserClaims.ModeratorId);
}
