using Domain.AggregateModels.ReportAggregate.Entities;
using System.Linq.Expressions;

namespace Application.Selectors.Reports;
public sealed class BookmarkInfo : ReportDto.BookmarkInfo, ISelector<Bookmark, BookmarkInfo>
{
    public Expression<Func<Bookmark, BookmarkInfo>> SetProjection() => e => new() { AccountId = e.AccountId };
}
