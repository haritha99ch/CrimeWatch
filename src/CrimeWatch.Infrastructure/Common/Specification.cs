using Microsoft.EntityFrameworkCore.Query;

namespace CrimeWatch.Infrastructure.Common;
public abstract class Specification<TEntity>(Expression<Func<TEntity, bool>>? criteria = null)
    where TEntity : BaseEntity
{
    public Expression<Func<TEntity, bool>>? Criteria { get; } = criteria;
    public List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; } = new();
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

    protected void AddInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include)
        => Includes.Add(include!);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        => OrderBy = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        => OrderByDescending = orderByDescendingExpression;
}
