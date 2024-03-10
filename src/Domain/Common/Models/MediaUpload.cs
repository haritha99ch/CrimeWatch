using Domain.AggregateModels.ReportAggregate.Enums;

namespace Domain.Common.Models;
public sealed record MediaUpload
{
    public required string FileName { get; init; }
    public required string Url { get; init; }
    public required MediaType MediaType { get; init; }

    public static MediaUpload Create(string fileName, string url, MediaType mediaType) =>
        new() { FileName = fileName, Url = url, MediaType = mediaType };
}
