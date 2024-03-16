using Domain.Common.Models;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Persistence.Contracts.Selectors;
public interface ISelector<TEntity, TResult>
    where TEntity : Entity
{
    protected Expression<Func<TEntity, TResult>> SetProjection();
    static Expression<Func<TPEntity, TPResult>> GetProjection<TPEntity, TPResult>()
        where TPEntity : Entity
        where TPResult : ISelector<TPEntity, TPResult>
        => ((TPResult)RuntimeHelpers.GetUninitializedObject(typeof(TPResult))).SetProjection();
}
