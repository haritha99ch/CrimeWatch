using CrimeWatch.Domain.Primitives;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CrimeWatch.Infrastructure.Primitives;
public abstract class Specification<T, V> where T : Entity<V> where V : ValueObject
{
    public Expression<Func<T, bool>>? Criteria { get; }
    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; } = new();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    protected Specification(Expression<Func<T, bool>> criteria) => Criteria = criteria;

    protected void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object?>> include
        ) => Includes.Add(include!);

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression
        ) => OrderBy = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression
        ) => OrderByDescending = orderByDescendingExpression;
}
