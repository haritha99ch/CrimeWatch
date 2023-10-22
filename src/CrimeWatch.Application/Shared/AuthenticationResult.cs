namespace CrimeWatch.Application.Shared;
public readonly struct AuthenticationResult
{
    private readonly ModeratorId? _moderatorId;
    private readonly WitnessId? _witnessId;
    private readonly bool _notRegistered = false;

    public bool IsModerator => _moderatorId is not null;
    public bool IsWitness => _witnessId is not null;
    public bool IsGuest => IsModerator && IsWitness;

    private AuthenticationResult(ModeratorId moderatorId)
    {
        _moderatorId = moderatorId;
    }

    private AuthenticationResult(WitnessId witnessId)
    {
        _witnessId = witnessId;
    }

    private AuthenticationResult(bool notRegistered)
    {
        _notRegistered = notRegistered;
    }

    public static implicit operator AuthenticationResult(ModeratorId moderatorId) => new(moderatorId);
    public static implicit operator AuthenticationResult(WitnessId witnessId) => new(witnessId);
    public static implicit operator AuthenticationResult(bool notRegistered) => new(notRegistered);

    public TResult Authorize<TResult>(
        Func<ModeratorId, TResult> moderatorId,
        Func<WitnessId, TResult> witnessId,
        Func<bool, TResult> notRegistered)
        => _moderatorId is not null ? moderatorId(_moderatorId) :
            _witnessId is not null ? witnessId(_witnessId) : notRegistered(_notRegistered);

    public TResult Authorize<TResult>(
        Func<ModeratorId, TResult> moderatorId,
        TResult? _ = default)
    {
        if (_moderatorId is not null) return moderatorId(_moderatorId);
        if (_ is null) throw new("Default behavior should be defined");
        return _;
    }

    public TResult Authorize<TResult>(
        Func<WitnessId, TResult> witnessId,
        TResult? _ = default)
    {
        if (_witnessId is not null) return witnessId(_witnessId);
        if (_ is null) throw new("Default behavior should be defined");
        return _;
    }
}
