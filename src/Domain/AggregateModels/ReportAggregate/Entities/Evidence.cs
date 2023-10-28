using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Entities;
public sealed record Evidence : Entity<EvidenceId>
{
    public required AccountId AuthorId { get; init; }
    public AccountId? ModeratorId { get; init; }
    public required string Caption { get; init; }
    public required string Description { get; init; }
    public required Location Location { get; init; }
    public required Status Status { get; init; }

    public List<MediaItem> MediaItems { get; init; } = new();
    public List<Comment> Comments { get; init; } = new();

    public Account? Author { get; init; }
    public Account? Moderator { get; init; }

    public static Evidence Create(
        AccountId authorId,
        string caption,
        string description,
        Location location,
        IEnumerable<MediaUpload> mediaItems) => new()
    {
        AuthorId = authorId,
        Caption = caption,
        Description = description,
        Location = location,
        MediaItems = MapMediaUploadsToEntities(mediaItems),
        Status = Status.Pending,
        CreatedAt = DateTime.Now,
        Id = new(Guid.NewGuid())
    };

    public Evidence Update(
            string caption,
            string description,
            Location location,
            List<MediaItem>? mediaItems,
            List<MediaUpload>? newMediaItems
        )
    {
        mediaItems ??= new();
        if (newMediaItems is not null)
        {
            mediaItems.AddRange(MapMediaUploadsToEntities(newMediaItems));
        }
        if (caption.Equals(Caption)
            && description.Equals(Description)
            && location.Equals(Location)
            && mediaItems.OrderBy(e => e).SequenceEqual(MediaItems.OrderBy(e => e)))
            return this;

        return this with
        {
            Caption = caption,
            Description = description,
            Location = location,
            MediaItems = mediaItems,
            UpdatedAt = DateTime.Now
        };
    }

    internal Evidence SetModerator(AccountId moderatorId)
        => this with { ModeratorId = moderatorId, Status = Status.UnderReview, UpdatedAt = DateTime.Now };

    internal Evidence SetApproved() => this with { Status = Status.Approved, UpdatedAt = DateTime.Now };
    internal Evidence SetDeclined() => this with { Status = Status.Declined, UpdatedAt = DateTime.Now };
    internal Evidence SetUnderReview() => this with { Status = Status.UnderReview, UpdatedAt = DateTime.Now };

    public Comment AddComment(AccountId accountId, string content)
    {
        var comment = Comment.Create(accountId, content);
        Comments.Add(comment);
        return comment;
    }

    public Comment UpdateComment(CommentId commentId, string content)
    {
        var comment = GetComment(commentId, out var index);
        var updatedComment = comment.Update(content);
        return ReplaceComment(index, updatedComment);
    }

    public bool DeleteComment(CommentId commentId)
    {
        var comment = GetComment(commentId, out var index);
        Comments.Remove(comment);
        return true;
    }

    private Comment ReplaceComment(int index, Comment updatedComment)
    {
        Comments[index] = updatedComment;
        return updatedComment;
    }

    private Comment GetComment(CommentId commentId, out int index)
    {
        var comment = Comments.FirstOrDefault(c => c.Id.Equals(commentId));
        if (comment is null) throw new("Comment is not found");
        index = Comments.IndexOf(comment);
        return comment;
    }


    private static List<MediaItem> MapMediaUploadsToEntities(IEnumerable<MediaUpload> mediaItems)
        => mediaItems.Select(e => MediaItem.Create(e.Url, e.MediaType)).ToList();
}
