using Domain.AggregateModels.ReportAggregate.Entities;
using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public record BookmarkInfo(AccountId AccountId) : Selector<Bookmark, BookmarkInfo>, ISelector
{
    protected override Expression<Func<Bookmark, BookmarkInfo>> SetProjection() => e => new(e.AccountId);
}
