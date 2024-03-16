using Domain.Common.Models;
using Persistence.Contracts.Selectors;

namespace Persistence.Helpers.Selectors;
public static class Adapter
{
    public static TResult Adapt<TEntity, TResult>(this TEntity entity)
        where TEntity : Entity
        where TResult : ISelector<TEntity, TResult>
    {
        var projector = Selector<TEntity, TResult>.GetProjection().Compile();
        return projector(entity);
    }
}
