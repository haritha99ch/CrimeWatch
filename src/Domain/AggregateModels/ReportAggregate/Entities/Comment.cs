using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Entities;
public sealed record Comment : Entity<CommentId>
{
    public required AccountId AccountId { get; init; }
    public required string Content { get; init; }

    public Account? Account { get; init; }

    public static Comment Create(AccountId accountId, string content) => new()
    {
        AccountId = accountId,
        Content = content,
        CreatedAt = DateTime.Now,
        Id = new(Guid.NewGuid())
    };

    public Comment Update(string content)
    {
        if (content.Equals(Content)) return this;
        return this with
        {
            Content = content,
            UpdatedAt = DateTime.Now
        };
    }
}
