using CrimeWatch.Infrastructure.Common;

namespace CrimeWatch.Infrastructure.Helpers;
internal static class SpecificationEvaluator
{
    internal static IQueryable<TEntity> AddSpecification<TEntity>
        (this IQueryable<TEntity> inputQuery, Specification<TEntity> specification) where TEntity : BaseEntity
    {
        var query = inputQuery;

        if (specification.Criteria != null)
            query = query.Where(specification.Criteria);

        query = specification.Includes
            .Aggregate(query, (current, include) => include(current));

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        return query;
    }
}
