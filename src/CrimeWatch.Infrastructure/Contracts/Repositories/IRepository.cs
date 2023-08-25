using CrimeWatch.Domain.Primitives;
using CrimeWatch.Infrastructure.Primitives;

namespace CrimeWatch.Infrastructure.Contracts.Repositories;
public interface IRepository<T, V> where T : Entity<V> where V : ValueObject
{
    Task<T?> GetByIdAsync(V id, CancellationToken? cancellationToken = null);
    Task<List<T>> GetAllAsync(CancellationToken? cancellationToken = null);
    Task<List<T>> GetAllByAsync(Specification<T, V> specification, CancellationToken? cancellationToken = null);
    Task<T?> GetByAsync(Specification<T, V> specification, CancellationToken? cancellationToken = null);
    Task<T> AddAsync(T entity, CancellationToken? cancellationToken = null);
    Task<T> UpdateAsync(T entity, CancellationToken? cancellationToken = null);
    Task<bool> DeleteByIdAsync(V id, CancellationToken? cancellationToken = null);
    Task<bool> ExistsAsync(Specification<T, V> specification, CancellationToken? cancellationToken = null);
    Task<int> CountAsync(CancellationToken? cancellationToken = null);
    Task<int> CountByAsync(Specification<T, V> specification, CancellationToken? cancellationToken = null);
    IRepository<T, V> AsTracking();
}
