using CrimeWatch.Domain.Entities;

namespace CrimeWatch.Application.Shared;
public readonly struct ModeratorOrWitness
{
    private readonly Moderator? _moderator;
    private readonly Witness? _witness;
    private readonly string? _error;

    private ModeratorOrWitness(Moderator moderator)
    {
        _moderator = moderator;
    }

    private ModeratorOrWitness(Witness witness)
    {
        _witness = witness;
    }

    private ModeratorOrWitness(string error)
    {
        _error = error;
    }

    public static implicit operator ModeratorOrWitness(Moderator moderator) => new(moderator);
    public static implicit operator ModeratorOrWitness(Witness witness) => new(witness);
    public static implicit operator ModeratorOrWitness(string error) => new(error);

    public TModeratorOrWitness Match<TModeratorOrWitness>(
        Func<Moderator, TModeratorOrWitness> moderator,
        Func<Witness, TModeratorOrWitness> witness,
        Func<string, TModeratorOrWitness> notFoundResult)
        => _moderator is not null ? moderator(_moderator) :
            _witness is not null ? witness(_witness) : notFoundResult(_error!);
}
