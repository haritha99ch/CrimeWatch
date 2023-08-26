using CrimeWatch.Domain.Primitives;
using CrimeWatch.Infrastructure.Contracts.Repositories;
using CrimeWatch.Infrastructure.Helpers;
using CrimeWatch.Infrastructure.Primitives;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CrimeWatch.Infrastructure.Repositories;
public class Repository<T, V> : IRepository<T, V> where T : Entity<V> where V : ValueObject
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;
    private IQueryable<T> _queryable;
    private bool _isTracking = false;

    public Repository(IApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _queryable = _dbSet.AsNoTracking();
    }

    public IRepository<T, V> AsTracking()
    {
        _queryable = _dbSet.AsTracking();
        _isTracking = true;
        return this;
    }

    public async Task<T> AddAsync(T entity, CancellationToken? cancellationToken = null)
    {
        EntityEntry<T> newEntity = await _dbSet.AddAsync(entity, cancellationToken ?? CancellationToken.None);
        await SaveChangesAsync();
        return newEntity.Entity;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken? cancellationToken = null)
    {
        List<T> entities = await _queryable.ToListAsync(cancellationToken ?? CancellationToken.None);
        if (_isTracking) SetDefaults();
        return entities ?? new();
    }

    public async Task<List<T>> GetAllByAsync(Specification<T, V> specification, CancellationToken? cancellationToken = null)
    {
        List<T>? entities = await _queryable.AddSpecification(specification).ToListAsync(cancellationToken ?? CancellationToken.None) ?? new();
        if (_isTracking) SetDefaults();
        return entities;
    }

    public async Task<T?> GetByAsync(Specification<T, V> specification, CancellationToken? cancellationToken = null)
    {
        T? entities = await _queryable.AddSpecification(specification).SingleOrDefaultAsync(cancellationToken ?? CancellationToken.None);
        if (_isTracking) SetDefaults();
        return entities;
    }

    public async Task<T?> GetByIdAsync(V id, CancellationToken? cancellationToken = null)
    {
        T? entity = await _dbSet.FindAsync(id, cancellationToken ?? CancellationToken.None);
        if (_isTracking) SetDefaults();
        return entity;
    }

    public async Task<int> CountAsync(CancellationToken? cancellationToken = null)
    {
        int count = await _dbSet.CountAsync(cancellationToken ?? CancellationToken.None);
        if (_isTracking) SetDefaults();
        return count;
    }

    public async Task<bool> ExistsAsync(Specification<T, V> specification, CancellationToken? cancellationToken = null)
    {
        bool isExists = await _dbSet.AddSpecification(specification).AnyAsync(cancellationToken ?? CancellationToken.None);
        if (_isTracking) SetDefaults();
        return isExists;
    }

    public async Task<int> CountByAsync(Specification<T, V> specification, CancellationToken? cancellationToken = null)
    {
        int count = await _queryable.AddSpecification(specification).CountAsync(cancellationToken ?? CancellationToken.None);
        if (_isTracking) SetDefaults();
        return count;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken? cancellationToken = null)
    {
        EntityEntry<T>? updatedEntity = _dbSet.Update(entity);
        await SaveChangesAsync();
        return updatedEntity.Entity;
    }

    public async Task<bool> DeleteByIdAsync(V id, CancellationToken? cancellationToken = null)
    {
        T? entity = await GetByIdAsync(id);
        if (entity == null) return false;
        _dbSet.Remove(entity);
        await SaveChangesAsync();
        if (await GetByIdAsync(id) == null)
            return true;
        return false;
    }

    private async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    private void SetDefaults()
    {
        _queryable = _dbSet.AsNoTracking();
        _isTracking = false;
    }
}
