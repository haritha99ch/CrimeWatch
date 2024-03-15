using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public sealed class ReportListItemDetails : ReportDto.ReportListItemDetails, ISelector<Report, ReportListItemDetails>
{
    public Expression<Func<Report, ReportListItemDetails>> SetProjection()
        => throw new NotImplementedException();
}
