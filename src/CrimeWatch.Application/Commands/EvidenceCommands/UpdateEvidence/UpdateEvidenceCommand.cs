﻿namespace CrimeWatch.Application.Commands.EvidenceCommands.UpdateEvidence;
public sealed record UpdateEvidenceCommand(
        EvidenceId Id,
        string Title,
        string Description,
        Location Location,
        string? MediaItems,
        List<IFormFile>? NewMediaItems
    ) : IRequest<Evidence>;
