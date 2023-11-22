using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Persistence.Common.Selectors;

/// <summary>
/// The EntitySelector is a base record used for dynamically generating selectors for entities.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
public abstract record Selector<TEntity, TResult> where TResult : Selector<TEntity, TResult>
{
    /// <summary>
    /// Abstract method to be implemented in derived classes to define the selection expression.
    /// </summary>
    protected abstract Expression<Func<TEntity, TResult>> Select();

    /// <summary>
    /// Static property to get the selector from derived classes.
    /// </summary>
    public static Expression<Func<TEntity, TResult>> GetSelector
        => ((TResult)RuntimeHelpers.GetUninitializedObject(typeof(TResult))).Select();
}
