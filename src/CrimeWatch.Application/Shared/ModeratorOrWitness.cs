using CrimeWatch.Domain.AggregateModels.ModeratorAggregate;
using CrimeWatch.Domain.AggregateModels.WitnessAggregate;

namespace CrimeWatch.Application.Shared;
public readonly struct ModeratorOrWitness
{
    private readonly Moderator? _moderator;
    private readonly Witness? _witness;

    private ModeratorOrWitness(Moderator moderator)
    {
        _moderator = moderator;
    }

    private ModeratorOrWitness(Witness witness)
    {
        _witness = witness;
    }

    public static implicit operator ModeratorOrWitness(Moderator moderator) => new(moderator);
    public static implicit operator ModeratorOrWitness(Witness witness) => new(witness);

    public TModeratorOrWitness Match<TModeratorOrWitness>(
        Func<Moderator, TModeratorOrWitness> moderator,
        Func<Witness, TModeratorOrWitness> witness)
        => _moderator is not null ? moderator(_moderator) : witness(_witness!);
}
