using Domain.Contracts.Models;
using Persistence.Common.Specifications.Types;

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
        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }
        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }
        if (specification.Pagination is Pagination pagination)
        {
            query = query.Skip(pagination.Skip).Take(pagination.Take);
        }
        return query;
    }
}
