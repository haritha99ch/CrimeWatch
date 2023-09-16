using CrimeWatch.Domain.Primitives;
using CrimeWatch.Infrastructure.Primitives;
using System.Linq.Expressions;

namespace CrimeWatch.Infrastructure.Contracts.Repositories;
public interface IRepository<T, V> where T : Entity<V> where V : ValueObject
{
    Task<T?> GetByIdAsync(V id, CancellationToken? cancellationToken = null);
    Task<List<T>> GetAllAsync(CancellationToken? cancellationToken = null);
    Task<List<T>> GetAllByAsync<S>(S specification, CancellationToken? cancellationToken = null) where S : Specification<T, V>;
    Task<List<TResult>> GetAllByAsync<S, TResult>(S specification, Expression<Func<T, TResult>> selector, CancellationToken? cancellationToken = null) where S : Specification<T, V>;
    Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken? cancellationToken = null);
    Task<TResult?> GetByIdAsync<TResult>(V id, Expression<Func<T, TResult>> selector, CancellationToken? cancellationToken = null);
    Task<T?> GetByAsync<S>(S specification, CancellationToken? cancellationToken = null) where S : Specification<T, V>;
    Task<TResult?> GetByAsync<S, TResult>(S specification, Expression<Func<T, TResult>> selector, CancellationToken? cancellationToken = null) where S : Specification<T, V>;
    Task<T> AddAsync(T entity, CancellationToken? cancellationToken = null);
    Task<T> UpdateAsync(T entity, CancellationToken? cancellationToken = null);
    Task<bool> DeleteByIdAsync(V id, CancellationToken? cancellationToken = null);
    Task<bool> ExistsAsync<S>(S specification, CancellationToken? cancellationToken = null) where S : Specification<T, V>;
    Task<int> CountAsync(CancellationToken? cancellationToken = null);
    Task<int> CountByAsync<S>(S specification, CancellationToken? cancellationToken = null) where S : Specification<T, V>;
    Task RemoveRangeAsync(List<T> entities, CancellationToken? cancellationToken = null);
    IRepository<T, V> AsTracking();
}
