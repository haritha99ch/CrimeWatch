using System.Runtime.CompilerServices;

namespace CrimeWatch.Infrastructure.Common;
public abstract record EntitySelector<TEntity, TSelector>
    where TEntity : BaseEntity where TSelector : EntitySelector<TEntity, TSelector>
{
    protected abstract Expression<Func<TEntity, TSelector>> MapSelector();

    public static Expression<Func<TEntity, TSelector>> Selector
        => ((TSelector)RuntimeHelpers.GetUninitializedObject(typeof(TSelector))).MapSelector();
}
