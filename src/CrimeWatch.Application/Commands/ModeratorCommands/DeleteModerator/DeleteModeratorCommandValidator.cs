namespace CrimeWatch.Application.Commands.ModeratorCommands.DeleteModerator;
public class DeleteModeratorCommandValidator : HttpContextValidator<DeleteModeratorCommand>
{
    public DeleteModeratorCommandValidator(IHttpContextAccessor contextAccessor) : base(contextAccessor)
    {
        RuleFor(x => x.ModeratorId)
            .MustAsync(HasPermission)
            .WithMessage("You do not have permission to update this moderator.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private Task<bool> HasPermission(ModeratorId moderatorId, CancellationToken cancellationToken)
        => Task.FromResult(UserClaims.ModeratorId != null && !UserClaims.ModeratorId.Equals(moderatorId));
}
