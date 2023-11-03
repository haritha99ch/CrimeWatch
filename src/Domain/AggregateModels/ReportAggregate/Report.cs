using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate;
public sealed record Report : AggregateRoot<ReportId>
{
    public required AccountId AuthorId { get; init; }
    public AccountId? ModeratorId { get; private set; }
    public required string Caption { get; set; }
    public required string Description { get; set; }
    public required Location Location { get; init; }
    public required Status Status { get; set; }

    public List<Evidence> Evidences { get; init; } = new();
    public List<Comment> Comments { get; init; } = new();
    public MediaItem? MediaItem { get; private init; }
    public Account? Author { get; init; }
    public Account? Moderator { get; init; }

    public static Report Create(
        AccountId authorId,
        string caption,
        string description,
        string no,
        string street1,
        string street2,
        string city,
        string province,
        MediaUpload mediaItem) => new()
    {
        Id = new(Guid.NewGuid()),
        AuthorId = authorId,
        Caption = caption,
        Description = description,
        Location = Location.Create(no, street1, street2, city, province),
        MediaItem = MediaItem.Create(mediaItem.Url, mediaItem.MediaType),
        Status = Status.Pending,
        CreatedAt = DateTime.Now
    };

    public void Update(string caption,
        string description,
        string no,
        string street1,
        string street2,
        string city,
        string province,
        MediaItem? mediaItem,
        MediaUpload? newMediaItem)
    {
        Location.Update(no, street1, street2, city, province);
        if (mediaItem is not null)
        {
            MediaItem!.Update(mediaItem.Url, mediaItem.MediaType);
        }
        else
        {
            if (newMediaItem is null) throw new("No new Media item");
            MediaItem!.Update(newMediaItem.Url, newMediaItem.MediaType);
        }

        if (caption.Equals(Caption) && description.Equals(Description)) return;

        Caption = caption;
        Description = description;
    }

    public void SetModerator(AccountId moderatorId)
    {
        ModeratorId = moderatorId;
        Status = Status.UnderReview;
        UpdatedAt = DateTime.Now;
    }

    public void SetApproved()
    {
        Status = Status.Approved;
        UpdatedAt = DateTime.Now;
    }

    public void SetDeclined()
    {
        Status = Status.Declined;
        UpdatedAt = DateTime.Now;
    }
    public void SetUnderReview()
    {
        Status = Status.UnderReview;
        UpdatedAt = DateTime.Now;
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
        var deleted = Comments.Remove(comment);
        return deleted;
    }

    public Evidence AddEvidence(
        AccountId authorId,
        string caption,
        string description,
        string no,
        string street1,
        string street2,
        string city,
        string province,
        IEnumerable<MediaUpload> mediaItems)
    {
        var evidence = Evidence.Create(authorId, caption, description, no, street1, street2, city, province,
            mediaItems);
        Evidences.Add(evidence);
        return evidence;
    }

    public bool DeleteEvidence(EvidenceId evidenceId)
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
        string no,
        string street1,
        string street2,
        string city,
        string province,
        List<MediaItem>? mediaItems,
        List<MediaUpload>? newMediaItems)
    {
        var evidence = GetEvidence(evidenceId);
        evidence.Update(caption, description, no, street1, street2, city, province, mediaItems, newMediaItems);
        return evidence;
    }

    public Evidence SetModeratorModeratorForEvidence(EvidenceId evidenceId, AccountId moderatorId)
    {
        var evidence = GetEvidence(evidenceId);
        evidence.SetModerator(moderatorId);
        return evidence;
    }

    public Evidence SetApproveEvidence(EvidenceId evidenceId)
    {
        var evidence = GetEvidence(evidenceId);
        evidence.SetApproved();
        return evidence;
    }

    public Evidence SetDeclineEvidence(EvidenceId evidenceId)
    {
        var evidence = GetEvidence(evidenceId);
        evidence.SetDeclined();
        return evidence;
    }

    public Evidence SetUnderReviewEvidence(EvidenceId evidenceId)
    {
        var evidence = GetEvidence(evidenceId);
        evidence.SetUnderReview();
        return evidence;
    }

    public Comment AddCommentToEvidence(EvidenceId evidenceId, AccountId accountId, string content)
    {
        var evidence = GetEvidence(evidenceId);
        var comment = evidence.AddComment(accountId, content);
        return comment;
    }

    public Comment UpdateCommentInEvidence(EvidenceId evidenceId, CommentId commentId, string content)
    {
        var evidence = GetEvidence(evidenceId);
        var comment = evidence.UpdateComment(commentId, content);
        return comment;
    }

    public bool DeleteCommentInEvidence(EvidenceId evidenceId, CommentId commentId)
    {
        var evidence = GetEvidence(evidenceId);
        var deleted = evidence.DeleteComment(commentId);
        return deleted;
    }

    private Evidence GetEvidence(EvidenceId evidenceId)
    {
        var evidence = Evidences.FirstOrDefault(e => e.Id.Equals(evidenceId));
        if (evidence is null) throw new("Evidence is not found");
        return evidence;
    }

    private Comment GetComment(CommentId commentId)
    {
        var comment = Comments.FirstOrDefault(c => c.Id.Equals(commentId));
        if (comment is null) throw new("Comment is not found");
        return comment;
    }
}
