using Domain.AggregateModels.AccountAggregate;
using Domain.AggregateModels.AccountAggregate.ValueObjects;
using Domain.AggregateModels.ReportAggregate.Entities;
using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.AggregateModels.ReportAggregate.Events;
using Domain.AggregateModels.ReportAggregate.ValueObjects;

namespace Domain.AggregateModels.ReportAggregate;
public sealed record Report : AggregateRoot<ReportId>
{
    public AccountId? AuthorId { get; init; }
    public AccountId? ModeratorId { get; private set; }
    public required string Caption { get; set; }
    public required string Description { get; set; }
    public required Location Location { get; init; }
    public required Status Status { get; set; }

    public List<Evidence> Evidences { get; init; } = new();
    public List<Comment> Comments { get; init; } = new();
    public List<ViolationType> ViolationTypes { get; private set; } = new();
    public List<Bookmark> Bookmarks { get; init; } = new();
    public int BookmarksCount { get; private set; }
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
        List<ViolationType> violationTypes,
        MediaUpload mediaItem)
    {
        var report = new Report
        {
            Id = new(Guid.NewGuid()),
            AuthorId = authorId,
            Caption = caption,
            Description = description,
            Location = Location.Create(no, street1, street2, city, province),
            MediaItem = MediaItem.Create(mediaItem.Url, mediaItem.MediaType),
            ViolationTypes = violationTypes,
            Status = Status.Pending,
            CreatedAt = DateTime.Now
        };
        report.RaiseDomainEvent(new ReportCreatedEvent(report));
        return report;
    }

    public void Update(string caption,
        string description,
        string? no,
        string street1,
        string? street2,
        string city,
        string province,
        List<ViolationType> violationTypes,
        MediaItem? mediaItem,
        MediaUpload? newMediaItem = null)
    {
        var violationTypesUpdated = false;
        bool mediaItemUpdated;
        var thisUpdated = false;
        if (!ViolationTypes.SequenceEqual(violationTypes))
        {
            ViolationTypes = violationTypes;
            violationTypesUpdated = true;
        }
        var locationUpdated = Location.Update(no, street1, street2, city, province);
        if (mediaItem is not null)
        {
            mediaItemUpdated = MediaItem!.Update(mediaItem.Url, mediaItem.MediaType);
        }
        else
        {
            if (newMediaItem is null) throw new("No new Media item");
            mediaItemUpdated = MediaItem!.Update(newMediaItem.Url, newMediaItem.MediaType);
        }

        if (!caption.Equals(Caption) || !description.Equals(Description))
        {
            Caption = caption;
            Description = description;
            thisUpdated = true;
        }

        if (!violationTypesUpdated && !locationUpdated && !mediaItemUpdated && !thisUpdated) return;
        UpdatedAt = DateTime.Now;
        RaiseDomainEvent(new ReportUpdatedEvent(this));
    }

    public void SetModerator(AccountId moderatorId)
    {
        if (ModeratorId is not null) throw new("Report is already moderated");
        ModeratorId = moderatorId;
        Status = Status.UnderReview;
        UpdatedAt = DateTime.Now;
        RaiseDomainEvent(new ReportModeratedEvent(this));
    }

    public void SetApproved()
    {
        if (Status.Equals(Status.Approved)) throw new("Report is already approved");
        Status = Status.Approved;
        UpdatedAt = DateTime.Now;
        RaiseDomainEvent(new ReportApprovedEvent(this));
    }

    public void SetDeclined()
    {
        if (Status.Equals(Status.Declined)) throw new("Report is already declined");
        Status = Status.Declined;
        UpdatedAt = DateTime.Now;
        RaiseDomainEvent(new ReportDeclinedEvent(this));
    }
    public void SetUnderReview()
    {
        if (Status.Equals(Status.UnderReview)) throw new("Report is already under Review");
        Status = Status.UnderReview;
        UpdatedAt = DateTime.Now;
        RaiseDomainEvent(new ReportRevertedToUnderReviewEvent(this));
    }

    public Comment AddComment(AccountId accountId, string content)
    {
        var comment = Comment.Create(accountId, content);
        Comments.Add(comment);
        RaiseDomainEvent(new CommentForReportAddedEvent(this, comment));
        return comment;
    }

    public Comment UpdateComment(CommentId commentId, string content)
    {
        var comment = GetComment(commentId);
        comment.Update(content);
        RaiseDomainEvent(new CommentFromReportUpdatedEvent(this, comment));
        return comment;
    }

    public bool RemoveComment(CommentId commentId)
    {
        var comment = GetComment(commentId);
        var deleted = Comments.Remove(comment);
        RaiseDomainEvent(new CommentFromReportRemovedEvent(this, commentId));
        return deleted;
    }

    public bool AddBookmark(AccountId accountId)
    {
        if (Bookmarks.Any(e => e.AccountId.Equals(accountId)))
        {
            throw new("Bookmark is already added");
        }
        var bookmark = Bookmark.Create(accountId);
        Bookmarks.Add(bookmark);
        BookmarksCount++;
        RaiseDomainEvent(new ReportBookmarkedEvent(this, bookmark));
        return true;
    }

    public bool RemoveBookmark(AccountId accountId)
    {
        if (!Bookmarks.Any(e => e.AccountId.Equals(accountId)))
        {
            throw new("No bookmark was added to remove");
        }
        Bookmarks.Remove(Bookmarks.FirstOrDefault(e => e.AccountId.Equals(accountId))!);
        BookmarksCount--;
        RaiseDomainEvent(new ReportBookmarkRemovedEvent(this, accountId));
        return true;
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
        RaiseDomainEvent(new EvidenceAddedForReportEvent(this, evidence));
        return evidence;
    }

    public bool RemoveEvidence(EvidenceId evidenceId)
    {
        var evidence = Evidences.FirstOrDefault(e => e.Id.Equals(evidenceId));
        if (evidence is null) return false;
        Evidences.Remove(evidence);
        RaiseDomainEvent(new EvidenceFromReportRemovedEvent(this, evidenceId));
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
        List<MediaUpload>? newMediaItems = null)
    {
        var evidence = GetEvidence(evidenceId);
        var evidenceUpdate = evidence.Update(caption, description, no, street1, street2, city, province, mediaItems,
            newMediaItems);
        if (evidenceUpdate) RaiseDomainEvent(new EvidenceFromReportUpdatedEvent(this, evidence));
        return evidence;
    }

    public Evidence SetModeratorModeratorForEvidence(EvidenceId evidenceId, AccountId moderatorId)
    {
        var evidence = GetEvidence(evidenceId);
        evidence.SetModerator(moderatorId);
        RaiseDomainEvent(new EvidenceFromReportModeratedEvent(this, evidence));
        return evidence;
    }

    public Evidence SetApproveEvidence(EvidenceId evidenceId)
    {
        var evidence = GetEvidence(evidenceId);
        evidence.SetApproved();
        RaiseDomainEvent(new EvidenceFromReportApprovedEvent(this, evidence));
        return evidence;
    }

    public Evidence SetDeclineEvidence(EvidenceId evidenceId)
    {
        var evidence = GetEvidence(evidenceId);
        evidence.SetDeclined();
        RaiseDomainEvent(new EvidenceFromReportDeclinedEvent(this, evidence));
        return evidence;
    }

    public Evidence SetUnderReviewEvidence(EvidenceId evidenceId)
    {
        var evidence = GetEvidence(evidenceId);
        evidence.SetUnderReview();
        RaiseDomainEvent(new EvidenceFromReportRevertedToUnderReviewEvent(this, evidence));
        return evidence;
    }

    public Comment AddCommentToEvidence(EvidenceId evidenceId, AccountId accountId, string content)
    {
        var evidence = GetEvidence(evidenceId);
        var comment = evidence.AddComment(accountId, content);
        RaiseDomainEvent(new CommentForEvidenceOnReportAddedEvent(this, evidenceId, comment));
        return comment;
    }

    public Comment UpdateCommentInEvidence(EvidenceId evidenceId, CommentId commentId, string content)
    {
        var evidence = GetEvidence(evidenceId);
        var comment = evidence.UpdateComment(commentId, content);
        RaiseDomainEvent(new CommentFromEvidenceOnReportUpdatedEvent(this, evidenceId, comment));
        return comment;
    }

    public bool RemoveCommentInEvidence(EvidenceId evidenceId, CommentId commentId)
    {
        var evidence = GetEvidence(evidenceId);
        var deleted = evidence.DeleteComment(commentId);
        RaiseDomainEvent(new CommentFromEvidenceOnReportRemovedEvent(this, evidenceId, commentId));
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
