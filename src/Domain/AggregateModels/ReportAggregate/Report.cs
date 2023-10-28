using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate;
public sealed record Report : AggregateRoot<ReportId>
{
    public required AccountId AuthorId { get; init; }
    public AccountId? ModeratorId { get; init; }
    public required string Caption { get; init; }
    public required string Description { get; init; }
    public required Location Location { get; init; }
    public required Status Status { get; init; }

    public List<Evidence> Evidences { get; init; } = new();
    public List<Comment> Comments { get; init; } = new();
    public MediaItem? MediaItem { get; init; }
    public Account? Author { get; init; }
    public Account? Moderator { get; init; }

    public static Report Create(
        AccountId authorId,
        string caption,
        string description,
        Location location,
        MediaUpload mediaItem) => new()
    {
        Id = new(Guid.NewGuid()),
        AuthorId = authorId,
        Caption = caption,
        Description = description,
        Location = location,
        MediaItem = MediaItem.Create(mediaItem.Url, mediaItem.MediaType),
        Status = Status.Pending,
        CreatedAt = DateTime.Now
    };

    public Report Update(string caption, string description, Location location, MediaItem? mediaItem,
        MediaUpload? newMediaItem)
    {
        MediaItem? updatedMediaItem;
        if (mediaItem is not null)
        {
            updatedMediaItem = MediaItem!.Update(mediaItem.Url, mediaItem.MediaType);
        }
        else
        {
            if (newMediaItem is null) throw new("No new Media item");
            updatedMediaItem = MediaItem!.Update(newMediaItem.Url, newMediaItem.MediaType);
        }

        if (caption.Equals(Caption)
            && description.Equals(Description)
            && location.Equals(Location)
            && updatedMediaItem.Equals(MediaItem)) return this;

        return this with
        {
            Caption = caption,
            Description = description,
            Location = location,
            MediaItem = updatedMediaItem
        };
    }

    public Report SetModerator(AccountId moderatorId)
        => this with { ModeratorId = moderatorId, Status = Status.UnderReview, UpdatedAt = DateTime.Now };

    public Report SetApproved() => this with { Status = Status.Approved, UpdatedAt = DateTime.Now };
    public Report SetDeclined() => this with { Status = Status.Declined, UpdatedAt = DateTime.Now };
    public Report SetUnderReview() => this with { Status = Status.UnderReview, UpdatedAt = DateTime.Now };

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
        var comment = GetComment(commentId, out _);
        Comments.Remove(comment);
        return true;
    }

    public Evidence AddEvidence(
        AccountId authorId,
        string caption,
        string description,
        Location location,
        IEnumerable<MediaUpload> mediaItems)
    {
        var evidence = Evidence.Create(authorId, caption, description, location, mediaItems);
        Evidences.Add(evidence);
        return evidence;
    }

    public bool RemoveEvidence(EvidenceId evidenceId)
    {
        var evidence = Evidences.FirstOrDefault(e => e.Id.Equals(evidenceId));
        if (evidence is null) return false;
        Evidences.Remove(evidence);
        return true;
    }

    public Evidence UpdateEvidence(
        EvidenceId evidenceId,
        string caption,
        string description,
        Location location,
        List<MediaItem>? mediaItems,
        List<MediaUpload>? newMediaItems)
    {
        var evidence = GetEvidence(evidenceId, out var index);
        var updatedEvidence = evidence.Update(caption, description, location, mediaItems, newMediaItems);
        return ReplaceEvidence(index, updatedEvidence);
    }

    public Evidence SetModeratorModeratorForEvidence(EvidenceId evidenceId, AccountId moderatorId)
    {
        var evidence = GetEvidence(evidenceId, out var index);
        var moderatedEvidence = evidence.SetModerator(moderatorId);
        return ReplaceEvidence(index, moderatedEvidence);
    }

    public Evidence SetApproveEvidence(EvidenceId evidenceId)
    {
        var evidence = GetEvidence(evidenceId, out var index);
        var approvedEvidence = evidence.SetApproved();
        return ReplaceEvidence(index, approvedEvidence);
    }

    public Evidence SetDeclineEvidence(EvidenceId evidenceId)
    {
        var evidence = GetEvidence(evidenceId, out var index);
        var declinedEvidence = evidence.SetDeclined();
        return ReplaceEvidence(index, declinedEvidence);
    }

    public Evidence SetUnderReviewEvidence(EvidenceId evidenceId)
    {
        var evidence = GetEvidence(evidenceId, out var index);
        var underReviewEvidence = evidence.SetUnderReview();
        return ReplaceEvidence(index, underReviewEvidence);
    }

    public Comment AddCommentToEvidence(EvidenceId evidenceId, AccountId accountId, string content)
    {
        var evidence = GetEvidence(evidenceId, out _);
        return evidence.AddComment(accountId, content);
    }

    public Comment UpdateCommentOnEvidence(EvidenceId evidenceId, CommentId commentId, string content)
    {
        var evidence = GetEvidence(evidenceId, out _);
        return evidence.UpdateComment(commentId, content);
    }

    public bool DeleteCommentOnEvidence(EvidenceId evidenceId, CommentId commentId)
    {
        var evidence = GetEvidence(evidenceId, out _);
        return evidence.DeleteComment(commentId);
    }

    private Evidence ReplaceEvidence(int index, Evidence newEvidence)
    {
        Evidences[index] = newEvidence;
        return newEvidence;
    }

    private Evidence GetEvidence(EvidenceId evidenceId, out int index)
    {
        var evidence = Evidences.FirstOrDefault(e => e.Id.Equals(evidenceId));
        if (evidence is null) throw new("Evidence is not found");
        index = Evidences.IndexOf(evidence);
        return evidence;
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
}
