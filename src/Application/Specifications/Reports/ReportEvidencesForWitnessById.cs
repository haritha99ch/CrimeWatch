﻿using Persistence.Common.Specifications;

namespace Application.Specifications.Reports;
internal sealed record ReportEvidencesForWitnessById : Specification<Report>
{

    public ReportEvidencesForWitnessById(ReportId reportId, AccountId currentAccountId)
        : base(e => e.Id.Equals(reportId)) { }
}
