using Domain.Contracts.Models;
using Persistence.Common.Results;

namespace Persistence.Common.Specifications.Helpers;
public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> AddSpecification<TEntity>(
            this IQueryable<TEntity> query,
            Specification<TEntity> specification
        )
        where TEntity : IAggregateRoot
    {
        query = specification.Includes.Aggregate(query, (current, include) => include(current));
        if (specification.Criteria is { } criteria)
        {
            query = query.Where(criteria);
        }
        if (specification.OrderBy is { } orderBy)
        {
            query = query.OrderBy(orderBy);
        }
        else if (specification.OrderByDescending is { } orderByDescending)
        {
            query = query.OrderByDescending(orderByDescending);
        }
        if (specification.Pagination is { } pagination)
        {
            query = query.Skip(pagination.Skip).Take(pagination.Take);
        }
        return query;
    }

    public static Query<TResult> AddSpecification<TEntity, TResult>(
            this IQueryable<TEntity> query,
            Specification<TEntity, TResult> specification
        )
        where TEntity : IAggregateRoot
    {
        query = specification.Includes.Aggregate(query, (current, include) => include(current));
        if (specification.Criteria is { } criteria)
        {
            query = query.Where(criteria);
        }
        if (specification.OrderBy is { } orderBy)
        {
            query = query.OrderBy(orderBy);
        }
        else if (specification.OrderByDescending is { } orderByDescending)
        {
            query = query.OrderByDescending(orderByDescending);
        }
        if (specification.Pagination is { } pagination)
        {
            query = query.Skip(pagination.Skip).Take(pagination.Take);
        }
        return specification.Select.Handle(
            e => new(query.Select(e)),
            e => new(query.Select(e)));
    }
}
