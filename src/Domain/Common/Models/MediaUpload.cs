using Domain.AggregateModels.ReportAggregate.Enums;

namespace Domain.Common.Models;

public sealed record MediaUpload
{
    public required string Url { get; init; }
    public required MediaType MediaType { get; init; }

    public static MediaUpload Create(string url, MediaType mediaType) =>
        new() { Url = url, MediaType = mediaType };
}
