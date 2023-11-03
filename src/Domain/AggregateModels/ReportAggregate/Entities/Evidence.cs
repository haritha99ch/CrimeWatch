using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate.Entities;
public sealed record Evidence : Entity<EvidenceId>
{
    public required AccountId AuthorId { get; init; }
    public AccountId? ModeratorId { get; set; }
    public required string Caption { get; set; }
    public required string Description { get; set; }
    public required Location Location { get; init; }
    public required Status Status { get; set; }

    public List<MediaItem> MediaItems { get; set; } = new();
    public List<Comment> Comments { get; init; } = new();

    public Account? Author { get; init; }
    public Account? Moderator { get; init; }

    public static Evidence Create(
        AccountId authorId,
        string caption,
        string description,
        string no,
        string street1,
        string street2,
        string city,
        string province,
        IEnumerable<MediaUpload> mediaItems) => new()
    {
        AuthorId = authorId,
        Caption = caption,
        Description = description,
        Location = Location.Create(no, street1, street2, city, province),
        MediaItems = MapMediaUploadsToEntities(mediaItems),
        Status = Status.Pending,
        CreatedAt = DateTime.Now,
        Id = new(Guid.NewGuid())
    };

    public void Update(
            string caption,
            string description,
            string no,
            string street1,
            string street2,
            string city,
            string province,
            List<MediaItem>? mediaItems,
            List<MediaUpload>? newMediaItems
        )
    {
        Location.Update(no, street1, street2, city, province);
        mediaItems ??= new();
        if (newMediaItems is not null)
        {
            mediaItems.AddRange(MapMediaUploadsToEntities(newMediaItems));
        }
        if (caption.Equals(Caption)
            && description.Equals(Description)
            && mediaItems.OrderBy(e => e).SequenceEqual(MediaItems.OrderBy(e => e)))
            return;

        Caption = caption;
        Description = description;
        MediaItems = mediaItems;
        UpdatedAt = DateTime.Now;
    }

    internal Evidence SetModerator(AccountId moderatorId)
    {
        ModeratorId = moderatorId;
        Status = Status.UnderReview;
        UpdatedAt = DateTime.Now;
        return this;
    }

    internal Evidence SetApproved()
    {
        Status = Status.Approved;
        UpdatedAt = DateTime.Now;
        return this;
    }

    internal Evidence SetDeclined()
    {
        Status = Status.Declined;
        UpdatedAt = DateTime.Now;
        return this;
    }

    internal Evidence SetUnderReview()
    {
        Status = Status.UnderReview;
        UpdatedAt = DateTime.Now;
        return this;
    }

    public Comment AddComment(AccountId accountId, string content)
    {
        var comment = Comment.Create(accountId, content);
        Comments.Add(comment);
        return comment;
    }

    public Comment UpdateComment(CommentId commentId, string content)
    {
        var comment = GetComment(commentId);
        comment.Update(content);
        return comment;
    }

    public bool DeleteComment(CommentId commentId)
    {
        var comment = GetComment(commentId);
        Comments.Remove(comment);
        return true;
    }

    private Comment GetComment(CommentId commentId)
    {
        var comment = Comments.FirstOrDefault(c => c.Id.Equals(commentId));
        if (comment is null) throw new("Comment is not found");
        return comment;
    }


    private static List<MediaItem> MapMediaUploadsToEntities(IEnumerable<MediaUpload> mediaItems)
    {
        return mediaItems.Select(e => MediaItem.Create(e.Url, e.MediaType)).ToList();
    }
}
