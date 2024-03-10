namespace Domain.Common.Results;
public readonly struct MediaItemUploadResult
{
    private readonly MediaUpload? _mediaUpload;
    private readonly Exception? _failed;

    private MediaItemUploadResult(MediaUpload mediaUpload)
    {
        _mediaUpload = mediaUpload;
    }

    private MediaItemUploadResult(Exception? failed)
    {
        _failed = failed;
    }

    public static implicit operator MediaItemUploadResult(MediaUpload mediaUpload) =>
        new(mediaUpload);

    public static implicit operator MediaItemUploadResult(Exception failed) => new(failed);

    public TResult Handle<TResult>(
            Func<MediaUpload, TResult> onMediaUpload,
            Func<Exception, TResult> onFailed
        ) => _failed is not null ? onFailed(_failed) : onMediaUpload(_mediaUpload!);
}
