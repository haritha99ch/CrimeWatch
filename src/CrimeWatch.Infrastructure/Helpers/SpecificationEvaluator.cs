using CrimeWatch.Domain.Primitives;
using CrimeWatch.Infrastructure.Primitives;

namespace CrimeWatch.Infrastructure.Helpers;
internal static class SpecificationEvaluator
{
    internal static IQueryable<T> GetQuery<T, V>(IQueryable<T> inputQuery, Specification<T, V> specification) where T : Entity<V> where V : ValueObject
    {
        IQueryable<T> query = inputQuery;

        if (specification.Criteria != null)
            query = query.Where(specification.Criteria);

        query = specification.Includes
            .Aggregate(query, (current, include) => include(current));

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
            query = query.OrderByDescending(specification.OrderByDescending);

        return query;
    }
}
