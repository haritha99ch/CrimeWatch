﻿namespace CrimeWatch.Application.Queries.EvidenceQueries.GetAllModeratedEvidenceForReport;
public sealed record GetAllModeratedEvidenceForReportQuery(ReportId ReportId) : IRequest<List<Evidence>>;
