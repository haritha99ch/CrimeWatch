using System.Linq.Expressions;
using Domain.Contracts.Models;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.Common.Specifications.Types;

namespace Persistence.Common.Specifications;

public abstract class Specification<TEntity>
    where TEntity : IAggregateRoot
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>
    > Includes { get; } = [ ];

    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
    public Pagination? Pagination { get; private set; }

    protected Specification(Expression<Func<TEntity, bool>>? criteria)
    {
        Criteria = criteria;
    }

    protected void AddInclude(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include
    ) => Includes.Add(include);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) =>
        OrderBy = orderByExpression;

    protected void AddOrderByDescending(
        Expression<Func<TEntity, object>> orderByDescendingExpression
    ) => OrderByDescending = orderByDescendingExpression;

    protected void AddPagination(Pagination pagination) => Pagination = pagination;
}
