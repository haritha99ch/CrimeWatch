using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Entities;
public sealed record Comment : Entity<CommentId>
{
    public required AccountId AuthorId { get; init; }
    public required string Content { get; set; }

    public Account? Account { get; init; }

    public static Comment Create(AccountId accountId, string content) =>
        new()
        {
            AuthorId = accountId,
            Content = content,
            CreatedAt = DateTime.Now,
            Id = new(Guid.NewGuid())
        };

    public void Update(string content)
    {
        if (content.Equals(Content)) return;

        Content = content;
        UpdatedAt = DateTime.Now;
    }
}
