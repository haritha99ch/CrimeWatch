using Domain.Contracts.Models;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.Common.Utilities;
using Shared.Contracts.Selectors;
using System.Linq.Expressions;

namespace Persistence.Common.Specifications;
public abstract class Specification<TEntity>
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

public abstract class Specification<TEntity, TResult>
    where TEntity : IAggregateRoot where TResult : ISelector
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; } = [];
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
    public Pagination? Pagination { get; private set; }
    public Expression<Func<TEntity, TResult>>? SelectSingle { get; }
    public Expression<Func<TEntity, List<TResult>>>? SelectList { get; }
    public Select<TEntity, TResult> Select { get; set; }

    protected Specification(Expression<Func<TEntity, bool>>? criteria = null)
    {
        Criteria = criteria;
    }

    protected void AddInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        => Includes.Add(include);

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) => OrderBy = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        => OrderByDescending = orderByDescendingExpression;

    protected void AddPagination(Pagination? pagination) => Pagination = pagination;
    protected void ProjectTo(Expression<Func<TEntity, TResult>> select) => Select = select;
    protected void ProjectTo(Expression<Func<TEntity, IEnumerable<TResult>>> select) => Select = select;
}
