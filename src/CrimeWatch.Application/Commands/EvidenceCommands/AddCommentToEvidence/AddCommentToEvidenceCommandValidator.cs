﻿using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.EvidenceCommands.AddCommentToEvidence;
public class AddCommentToEvidenceCommandValidator : HttpContextValidator<AddCommentToEvidenceCommand>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public AddCommentToEvidenceCommandValidator(
        IAuthenticationService authenticationService,
        IRepository<Evidence, EvidenceId> evidenceRepository) : base(authenticationService)
    {
        _evidenceRepository = evidenceRepository;
        RuleFor(e => e)
            .MustAsync(HasPermissions)
            .WithMessage("You do not have permission to moderate this evidence.")
            .WithErrorCode(StatusCodes.Status401Unauthorized.ToString());
    }

    private async Task<bool> HasPermissions(AddCommentToEvidenceCommand command, CancellationToken cancellationToken)
    {
        var result = _authenticationService.Authenticate();
        return await result.Authorize<Task<bool>>(
            async moderatorId
                => await _evidenceRepository.HasPermissionsToModerateAsync(command.EvidenceId, moderatorId,
                    cancellationToken),
            Task.FromResult(false));
    }
}
