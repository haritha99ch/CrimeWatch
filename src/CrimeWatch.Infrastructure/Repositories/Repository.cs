using CrimeWatch.Infrastructure.Common;
using CrimeWatch.Infrastructure.Contracts.Repositories;
using CrimeWatch.Infrastructure.Helpers;

namespace CrimeWatch.Infrastructure.Repositories;
public class Repository<TEntity, TValueObject> : IRepository<TEntity, TValueObject> where TEntity : Entity<TValueObject>
    where TValueObject : ValueObject
{
    private readonly IApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private IQueryable<TEntity> _queryable;
    private bool _isTracking;

    private bool IsTracking
    {
        get => _isTracking;
        set
        {
            _isTracking = value;
            _queryable = value ? _dbSet.AsTracking() : _dbSet.AsNoTracking();
        }
    }

    public Repository(IApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
        _queryable = _dbSet.AsNoTracking();
        IsTracking = false;
    }

    public IRepository<TEntity, TValueObject> AsTracking()
    {
        IsTracking = true;
        return this;
    }


    #region Common

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken? cancellationToken = null)
    {
        var newEntity = await _dbSet.AddAsync(entity, cancellationToken ?? CancellationToken.None);
        await SaveChangesAsync(cancellationToken);
        _context.ChangeTracker.Clear();
        return newEntity.Entity;
    }

    public async Task<TEntity?> GetByIdAsync(TValueObject id, CancellationToken? cancellationToken = null)
    {
        var entity =
            await _queryable.FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken? cancellationToken = null)
    {
        var entities = await _queryable.ToListAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return entities;
    }

    public async Task<bool> ExistsByIdAsync(TValueObject id, CancellationToken? cancellationToken = null)
    {
        var isExists = await _queryable.AnyAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return isExists;
    }

    public async Task<int> CountAsync(CancellationToken? cancellationToken = null)
    {
        var count = await _queryable.CountAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return count;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken? cancellationToken = null)
    {
        if (_dbSet.Local.Any(g => g.Id == entity.Id))
        {
            await SaveChangesAsync(cancellationToken);
            return entity;
        }
        var updatedEntity = _dbSet.Update(entity);
        await SaveChangesAsync(cancellationToken);
        return updatedEntity.Entity;
    }

    public async Task<bool> DeleteByIdAsync(TValueObject id, CancellationToken? cancellationToken = null)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        _dbSet.Remove(entity);
        await SaveChangesAsync(cancellationToken);
        return !await ExistsByIdAsync(id);
    }

    #endregion


    #region Specification

    public async Task<TEntity?> GetByIdAsync<TSpecification>(TValueObject id,
        CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>
    {
        var specification = Activator.CreateInstance<TSpecification>();
        var entity = await _queryable.AddSpecification(specification)
            .FirstOrDefaultAsync(e => e.Id.Equals(id), cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return entity;

    }

    public async Task<List<TEntity>> GetAllAsync<TSpecification>(
        CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>
    {
        var specification = Activator.CreateInstance<TSpecification>();
        var entities = await _queryable.AddSpecification(specification)
            .ToListAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return entities;
    }

    public async Task<TEntity?> GetOneAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>
    {
        var entities = await _queryable.AsNoTracking().AddSpecification(specification)
            .FirstOrDefaultAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return entities;
    }

    public async Task<List<TEntity>> GetManyAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>
    {
        var entities = await _queryable.AddSpecification(specification)
            .ToListAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return entities;
    }

    public async Task<bool> ExistsAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>
    {
        var isExists = await _dbSet.AddSpecification(specification)
            .AnyAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return isExists;
    }

    public async Task<int> CountByAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>
    {
        var count = await _queryable.AddSpecification(specification)
            .CountAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return count;
    }

    #endregion


    #region Selector

    public async Task<TResult?> GetByIdAsync<TResult>(TValueObject id, Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null)
    {
        var result = await _queryable.Where(e => e.Id.Equals(id)).Select(selector)
            .SingleOrDefaultAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return result;
    }

    public async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null)
    {
        var entities = await _queryable.Select(selector).ToListAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return entities;
    }

    public async Task<TResult?> GetOneAsync<TResult, TSpecification>(TSpecification specification,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity, TValueObject>
    {
        var entities = await _queryable.AddSpecification(specification).Select(selector)
            .SingleOrDefaultAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return entities;
    }

    public async Task<List<TResult>> GetManyAsync<TResult, TSpecification>(TSpecification specification,
        Expression<Func<TEntity, TResult>> selector, CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>
    {
        var entities = await _queryable.AddSpecification(specification).Select(selector)
            .ToListAsync(cancellationToken ?? CancellationToken.None);
        if (IsTracking) SetDefaults();
        return entities;
    }

    #endregion


    private async Task SaveChangesAsync(CancellationToken? cancellationToken = null)
        => await _context.SaveChangesAsync(cancellationToken ?? CancellationToken.None);

    private void SetDefaults()
    {
        IsTracking = false;
    }

    public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken? cancellationToken = null)
    {
        _dbSet.RemoveRange(entities);
        await SaveChangesAsync();
    }


}
