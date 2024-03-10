namespace Application.Errors.Files;
public record FileUploadError : Error<FileUploadError>
{

    public override string Title { get; init; } = "Cannot upload the file.";
    public override string Message { get; init; } = "File could not be uploaded.";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.InternalServerError;
}
