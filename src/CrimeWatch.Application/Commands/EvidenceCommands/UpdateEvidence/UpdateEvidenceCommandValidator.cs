using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.EvidenceCommands.UpdateEvidence;
public class UpdateEvidenceCommandValidator : HttpContextValidator<UpdateEvidenceCommand>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public UpdateEvidenceCommandValidator(
        IAuthenticationService authenticationService,
        IRepository<Evidence, EvidenceId> evidenceRepository) : base(authenticationService)
    {
        _evidenceRepository = evidenceRepository;
        RuleFor(e => e)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to update this evidence.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(UpdateEvidenceCommand command, CancellationToken cancellationToken)
    {
        var result = _authenticationService.Authenticate();
        return await result.Authorize<Task<bool>>(
            async witnessId
                => await _evidenceRepository.HasPermissionsToEditAsync(command.Id, witnessId, cancellationToken),
            Task.FromResult(false));
    }
}
