using Domain.AggregateModels.ReportAggregate;
using Domain.AggregateModels.ReportAggregate.Enums;

namespace Domain.Test.AggregateModels;
[TestClass]
public class ReportAggregate
{
    [TestMethod]
    public void Create_Report()
    {
        var caption = DataProvider.Caption;
        var description = DataProvider.Description;
        var no = DataProvider.No;
        var street1 = DataProvider.Street1;
        var street2 = DataProvider.Street2;
        var city = DataProvider.City;
        var province = DataProvider.Province;
        var authorId = DataProvider.AuthorId;
        var mediaItemUpload = DataProvider.TestMediaItem;
        var violationTypes = DataProvider.ViolationTypes;

        var report = Report.Create(authorId, caption, description, no, street1, street2, city, province, violationTypes,
            mediaItemUpload);

        Assert.IsNotNull(report);
        Assert.IsNotNull(report.MediaItem);
        Assert.AreEqual(authorId, report.AuthorId);
        Assert.AreEqual(caption, report.Caption);
        Assert.AreEqual(description, report.Description);
        Assert.AreEqual(no, report.Location.No);
        Assert.AreEqual(street1, report.Location.Street1);
        Assert.AreEqual(street2, report.Location.Street2);
        Assert.AreEqual(city, report.Location.City);
        Assert.AreEqual(province, report.Location.Province);
        Assert.AreEqual(violationTypes, report.ViolationTypes);
    }

    [TestMethod]
    public void Update_Report()
    {
        var report = DataProvider.TestReport;
        var updatedCaption = DataProvider.Caption;
        var updatedDescription = DataProvider.Description;
        var no = report.Location.No!;
        var street1 = report.Location.Street1;
        var updatedStreet2 = DataProvider.Street2;
        var city = report.Location.City;
        var province = report.Location.Province;
        var newMediaItem = DataProvider.TestMediaItem;
        var violationTypes = DataProvider.ViolationTypes;

        report.Update(updatedCaption, updatedDescription, no, street1, updatedStreet2, city, province, violationTypes,
            null, newMediaItem);

        Assert.IsNotNull(report.MediaItem);
        Assert.IsNotNull(report.AuthorId);
        Assert.IsNull(report.ModeratorId);
        Assert.AreEqual(updatedCaption, report.Caption);
        Assert.AreEqual(updatedDescription, report.Description);
        Assert.AreEqual(no, report.Location.No);
        Assert.AreEqual(street1, report.Location.Street1);
        Assert.AreEqual(updatedStreet2, report.Location.Street2);
        Assert.AreEqual(city, report.Location.City);
        Assert.AreEqual(province, report.Location.Province);
        Assert.AreEqual(Status.Pending, report.Status);
        Assert.AreSame(violationTypes, report.ViolationTypes);
    }

    [TestMethod]
    public void Moderate_Report()
    {
        var report = DataProvider.TestReport;
        var moderatorId = DataProvider.ModeratorId;

        report.SetModerator(moderatorId);

        Assert.IsNotNull(report.ModeratorId);
        Assert.AreEqual(moderatorId, report.ModeratorId);
        Assert.AreEqual(Status.UnderReview, report.Status);
        Assert.IsNotNull(report.UpdatedAt);
    }

    [TestMethod]
    public void Approve_Report()
    {
        var report = DataProvider.TestModeratedReport;

        report.SetApproved();

        Assert.AreEqual(Status.Approved, report.Status);
        Assert.IsNotNull(report.UpdatedAt);
    }

    [TestMethod]
    public void Decline_Report()
    {
        var report = DataProvider.TestModeratedReport;

        report.SetDeclined();

        Assert.AreEqual(Status.Declined, report.Status);
        Assert.IsNotNull(report.UpdatedAt);
    }

    [TestMethod]
    public void Revert_Report_To_UnderReview()
    {
        var report = DataProvider.TestApprovedReport;

        report.SetUnderReview();

        Assert.AreEqual(Status.UnderReview, report.Status);
        Assert.IsNotNull(report.UpdatedAt);
    }

    [TestMethod]
    public void Add_Comment_To_Report()
    {
        var report = DataProvider.TestReport;
        var comment = DataProvider.Description;
        var authorId = DataProvider.AuthorId;

        report.AddComment(authorId, comment);

        Assert.AreEqual(report.Comments.Count, 1);
        Assert.AreEqual(report.Comments[0].AuthorId, authorId);
        Assert.AreEqual(report.Comments[0].Content, comment);
        Assert.IsNotNull(report.Comments[0].CreatedAt);
    }

    [TestMethod]
    public void Update_Comment_In_Report()
    {
        var report = DataProvider.TestReportWithAComment;
        var updatedComment = DataProvider.Description;

        report.UpdateComment(report.Comments[0].Id, updatedComment);

        Assert.AreEqual(1, report.Comments.Count);
        Assert.AreEqual(updatedComment, report.Comments[0].Content);
    }

    [TestMethod]
    public void Delete_Comment_In_Report()
    {
        var report = DataProvider.TestReportWithAComment;

        var deleted = report.DeleteComment(report.Comments[0].Id);

        Assert.IsTrue(deleted);
        Assert.AreEqual(0, report.Comments.Count);
    }

    [TestMethod]
    public void Add_Evidence_To_Report()
    {
        var report = DataProvider.TestReport;
        var authorId = DataProvider.AuthorId;
        var caption = DataProvider.Caption;
        var description = DataProvider.Description;
        var no = DataProvider.No;
        var street1 = DataProvider.Street1;
        var street2 = DataProvider.Street2;
        var city = DataProvider.City;
        var province = DataProvider.Province;
        var mediaItems = Enumerable.Repeat(DataProvider.TestMediaItem, 3).ToList();

        report.AddEvidence(authorId, caption, description, no, street1, street2, city, province, mediaItems);

        Assert.AreEqual(1, report.Evidences.Count);
        Assert.AreEqual(authorId, report.Evidences[0].AuthorId);
        Assert.AreEqual(caption, report.Evidences[0].Caption);
        Assert.AreEqual(description, report.Evidences[0].Description);
        Assert.IsNotNull(report.Evidences[0].CreatedAt);
        Assert.AreEqual(no, report.Evidences[0].Location.No);
        Assert.AreEqual(street1, report.Evidences[0].Location.Street1);
        Assert.AreEqual(street2, report.Evidences[0].Location.Street2);
        Assert.AreEqual(city, report.Evidences[0].Location.City);
        Assert.AreEqual(province, report.Evidences[0].Location.Province);
        Assert.AreEqual(3, report.Evidences[0].MediaItems.Count);
    }

    [TestMethod]
    public void Update_Evidence_In_Report()
    {
        var report = DataProvider.TestReportWithAEvidence;
        var updatedCaption = DataProvider.Caption;
        var updatedDescription = DataProvider.Description;
        var no = report.Location.No!;
        var street1 = report.Location.Street1;
        var updatedStreet2 = DataProvider.Street2;
        var city = report.Location.City;
        var province = report.Location.Province;
        var newMediaItems = Enumerable.Repeat(DataProvider.TestMediaItem, 3).ToList();
        var exitingMediaItems = report.Evidences[0].MediaItems;
        exitingMediaItems.RemoveAt(1);

        report.UpdateEvidence(report.Evidences[0].Id, updatedCaption, updatedDescription, no, street1, updatedStreet2,
            city, province,
            exitingMediaItems, newMediaItems);

        Assert.IsNotNull(report.MediaItem);
        Assert.IsNotNull(report.AuthorId);
        Assert.IsNull(report.ModeratorId);
        Assert.AreEqual(updatedCaption, report.Evidences[0].Caption);
        Assert.AreEqual(updatedDescription, report.Evidences[0].Description);
        Assert.AreEqual(no, report.Evidences[0].Location.No);
    }

    [TestMethod]
    public void Moderate_Evidence_In_Report()
    {
        var report = DataProvider.TestReportWithAEvidence;
        var moderatorId = DataProvider.ModeratorId;

        report.SetModeratorModeratorForEvidence(report.Evidences[0].Id, moderatorId);

        Assert.IsNotNull(report.Evidences[0].ModeratorId);
        Assert.AreEqual(moderatorId, report.Evidences[0].ModeratorId);
        Assert.AreEqual(Status.UnderReview, report.Evidences[0].Status);
    }

    [TestMethod]
    public void Approve_Evidence_In_Report()
    {
        var report = DataProvider.TestReportWithAEvidence;

        report.SetApproveEvidence(report.Evidences[0].Id);

        Assert.AreEqual(Status.Approved, report.Evidences[0].Status);
    }

    [TestMethod]
    public void Decline_Evidence_In_Report()
    {
        var report = DataProvider.TestReportWithAEvidence;

        report.SetDeclineEvidence(report.Evidences[0].Id);

        Assert.AreEqual(Status.Declined, report.Evidences[0].Status);
    }

    [TestMethod]
    public void Revert_Evidence_To_UnderReview()
    {
        var report = DataProvider.TestReportWithAEvidence;

        report.SetUnderReviewEvidence(report.Evidences[0].Id);

        Assert.AreEqual(Status.UnderReview, report.Evidences[0].Status);
    }

    [TestMethod]
    public void Delete_Evidence_In_Report()
    {
        var report = DataProvider.TestReportWithAEvidence;

        var deleted = report.DeleteEvidence(report.Evidences[0].Id);

        Assert.IsTrue(deleted);
        Assert.AreEqual(0, report.Evidences.Count);
    }


    [TestMethod]
    public void Add_Comment_To_Evidence_In_Report()
    {
        var report = DataProvider.TestReportWithAEvidence;
        var comment = DataProvider.Description;
        var authorId = DataProvider.AuthorId;

        report.AddCommentToEvidence(report.Evidences[0].Id, authorId, comment);

        Assert.AreEqual(1, report.Evidences[0].Comments.Count);
        Assert.AreEqual(authorId, report.Evidences[0].Comments[0].AuthorId);
        Assert.AreEqual(comment, report.Evidences[0].Comments[0].Content);
        Assert.IsNotNull(report.Evidences[0].Comments[0].CreatedAt);
    }

    [TestMethod]
    public void Update_Comment_In_Evidence_In_Report()
    {
        var report = DataProvider.TestReportWithAEvidenceIncludingComment;
        var updatedComment = DataProvider.Description;

        report.UpdateCommentInEvidence(report.Evidences[0].Id, report.Evidences[0].Comments[0].Id, updatedComment);

        Assert.AreEqual(1, report.Evidences[0].Comments.Count);
        Assert.AreEqual(updatedComment, report.Evidences[0].Comments[0].Content);
    }

    [TestMethod]
    public void Delete_Comment_In_Evidence_In_Report()
    {
        var report = DataProvider.TestReportWithAEvidenceIncludingComment;

        var deleted = report.DeleteCommentInEvidence(report.Evidences[0].Id, report.Evidences[0].Comments[0].Id);

        Assert.IsTrue(deleted);
        Assert.AreEqual(0, report.Evidences[0].Comments.Count);
    }
}
