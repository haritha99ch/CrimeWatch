using Domain.Contracts.Models;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.Common.Specifications.Types;
using System.Linq.Expressions;

namespace Persistence.Common.Specifications;
public abstract record Specification<TEntity>
    where TEntity : IAggregateRoot
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; } = [];

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

public abstract record Specification<TEntity, TResult>
    where TEntity : IAggregateRoot
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; } = [];

    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
    public Pagination? Pagination { get; private set; }
    public Expression<Func<TEntity, TResult>>? Select { get; protected init; }
    public Expression<Func<TEntity, List<TResult>>>? SelectList { get; protected init; }

    protected Specification(Expression<Func<TEntity, bool>>? criteria = null)
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

    protected void AddPagination(Pagination? pagination) => Pagination = pagination;

}
