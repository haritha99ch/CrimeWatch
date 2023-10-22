using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.ModeratorCommands.UpdateModerator;
public class UpdateModeratorCommandValidator : HttpContextValidator<UpdateModeratorCommand>
{
    public UpdateModeratorCommandValidator(IAuthenticationService authenticationService) : base(authenticationService)
    {
        RuleFor(x => x.Id)
            .Must(HasPermission)
            .WithMessage("You do not have permission to update this moderator.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private bool HasPermission(ModeratorId moderatorId)
        => _authenticationService.Authenticate()
            .Authorize(moderatorId: moderatorId.Equals);
}
