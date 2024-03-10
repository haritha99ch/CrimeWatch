using Domain.AggregateModels.ReportAggregate.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace TestDataProvider;
internal class MockFormFile : IFormFile
{
    private readonly byte[] _content;
    public string ContentType { get; }
    public string ContentDisposition { get; }
    public IHeaderDictionary Headers { get; }
    public long Length { get; } = default;
    public string Name { get; }
    public string FileName { get; }

    public MockFormFile(byte[] content, MediaType contentType)
    {
        _content = content;
        ContentType = contentType switch
        {
            MediaType.Audio => "audio/mpeg",
            MediaType.Video => "video/mp4",
            MediaType.Image => "image/jpeg",
            MediaType.Document => "application/pdf",
            _ => throw new ArgumentException("Unsupported media type")
        };
        Name = GetFileName(contentType);
        FileName = GetFileName(contentType);
        ContentDisposition = $"form-data; name=\"file\"; filename=\"{FileName}\"";
        Headers = new HeaderDictionary
        {
            { "Content-Type", new StringValues(ContentType) },
            { "Content-Disposition", new StringValues(ContentDisposition) }
        };
    }

    public Stream OpenReadStream() => new MemoryStream(_content);
    public void CopyTo(Stream target)
    {
        using var stream = new MemoryStream(_content);
        stream.CopyToAsync(target);
    }
    public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = new())
    {
        using var stream = new MemoryStream(_content);
        await stream.CopyToAsync(target, cancellationToken);
    }

    private static string GetFileName(MediaType contentType)
    {
        return contentType switch
        {
            MediaType.Audio => "file.mp3",
            MediaType.Video => "file.mp4",
            MediaType.Image => "file.jpg",
            MediaType.Document => "file.pdf",
            _ => throw new ArgumentException("Unsupported media type")
        };
    }
}
