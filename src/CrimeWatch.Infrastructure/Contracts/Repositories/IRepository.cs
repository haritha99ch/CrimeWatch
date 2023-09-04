using CrimeWatch.Domain.Primitives;
using CrimeWatch.Infrastructure.Primitives;

namespace CrimeWatch.Infrastructure.Contracts.Repositories;
public interface IRepository<T, V> where T : Entity<V> where V : ValueObject
{
    Task<T?> GetByIdAsync(V id, CancellationToken? cancellationToken = null);
    Task<List<T>> GetAllAsync(CancellationToken? cancellationToken = null);
    Task<List<T>> GetAllByAsync<S>(S specification, CancellationToken? cancellationToken = null) where S : Specification<T, V>;
    Task<T?> GetByAsync<S>(S specification, CancellationToken? cancellationToken = null) where S : Specification<T, V>;
    Task<T> AddAsync(T entity, CancellationToken? cancellationToken = null);
    Task<T> UpdateAsync(T entity, CancellationToken? cancellationToken = null);
    Task<bool> DeleteByIdAsync(V id, CancellationToken? cancellationToken = null);
    Task<bool> ExistsAsync<S>(S specification, CancellationToken? cancellationToken = null) where S : Specification<T, V>;
    Task<int> CountAsync(CancellationToken? cancellationToken = null);
    Task<int> CountByAsync<S>(S specification, CancellationToken? cancellationToken = null) where S : Specification<T, V>;
    Task RemoveRangeAsync(List<T> entities, CancellationToken? cancellationToken = null);
    IRepository<T, V> AsTracking();
}
