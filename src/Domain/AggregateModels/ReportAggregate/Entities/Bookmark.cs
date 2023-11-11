using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Entities;
public sealed record Bookmark : Entity<BookmarkId>
{
    public required AccountId AccountId { get; init; }

    public Account? Account { get; init; }

    public static Bookmark Create(AccountId accountId) => new()
    {
        Id = new(Guid.NewGuid()),
        AccountId = accountId,
        CreatedAt = DateTime.Now
    };
}
