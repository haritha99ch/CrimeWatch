namespace Application.Errors.Files;
public record FileDeleteError : Error<FileDeleteError>
{
    public override string Title { get; init; } = "File delete failed.";
    public override string Message { get; init; } = "File cannot be deleted.";
    public override HttpStatusCode Code { get; init; } = HttpStatusCode.Conflict;
}
