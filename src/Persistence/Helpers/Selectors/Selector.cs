using Domain.Common.Models;
using Persistence.Contracts.Selectors;
using System.Linq.Expressions;

namespace Persistence.Helpers.Selectors;
public static class Selector<TEntity, TResult>
    where TEntity : Entity
    where TResult : ISelector<TEntity, TResult>
{
    public static Expression<Func<TEntity, TResult>> GetProjection()
        => ISelector<TEntity, TResult>.GetProjection<TEntity, TResult>();
}
