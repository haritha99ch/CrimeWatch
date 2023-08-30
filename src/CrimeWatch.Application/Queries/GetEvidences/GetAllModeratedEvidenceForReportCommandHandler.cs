﻿using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Queries.GetEvidences;
internal class GetAllModeratedEvidenceForReportCommandHandler : IRequestHandler<GetAllModeratedEvidenceForReportCommand, List<Evidence>>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public GetAllModeratedEvidenceForReportCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<List<Evidence>> Handle(GetAllModeratedEvidenceForReportCommand request, CancellationToken cancellationToken)
        => await _evidenceRepository.GetModeratedEvidencesWithAllForReportAsync(request.ReportId, cancellationToken);
}
