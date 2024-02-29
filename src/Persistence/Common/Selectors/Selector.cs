using Domain.Common.Models;
using Persistence.Contracts.Selectors;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Persistence.Common.Selectors;
public abstract record Selector<TEntity, TResult>
    where TEntity : Entity
    where TResult : Selector<TEntity, TResult>, ISelector
{
    protected abstract Expression<Func<TEntity, TResult>> SetProjection();

    /// <summary>
    ///     Static property to get the selector from derived classes.
    /// </summary>
    public static Expression<Func<TEntity, TResult>> Projection
        => ((TResult)RuntimeHelpers.GetUninitializedObject(typeof(TResult))).SetProjection();
}
