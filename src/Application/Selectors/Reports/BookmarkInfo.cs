using Domain.AggregateModels.ReportAggregate.Entities;
using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public record BookmarkInfo(AccountId AccountId) : ISelector<Bookmark, BookmarkInfo>
{
    public Expression<Func<Bookmark, BookmarkInfo>> SetProjection() => e => new(e.AccountId);
}
