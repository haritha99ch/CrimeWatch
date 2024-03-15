using Domain.Common.Models;
using System.Linq.Expressions;

namespace Persistence.Contracts.Selectors;
public interface ISelector<TEntity, TResult>
    where TEntity : Entity
{
    protected Expression<Func<TEntity, TResult>> SetProjection();
    internal Expression<Func<TEntity, TResult>> Projection => SetProjection();
}
