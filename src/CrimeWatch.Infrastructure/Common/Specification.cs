using CrimeWatch.Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CrimeWatch.Infrastructure.Common;
public abstract class Specification<TEntity, TValueObject>
    where TEntity : Entity<TValueObject> where TValueObject : ValueObject
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; } = new();
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

    protected Specification(Expression<Func<TEntity, bool>>? criteria = null)
    {
        Criteria = criteria;
    }

    protected void AddInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include)
        => Includes.Add(include!);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        => OrderBy = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        => OrderByDescending = orderByDescendingExpression;
}
