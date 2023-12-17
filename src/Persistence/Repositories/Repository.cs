using Domain.Common.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Persistence.Common.Selectors;
using Persistence.Common.Specifications;
using Persistence.Common.Specifications.Helpers;
using Persistence.Contracts.Repositories;
using System.Linq.Expressions;

namespace Persistence.Repositories;
internal class Repository<TEntity, TEntityId> : IRepository<TEntity, TEntityId>
    where TEntity : AggregateRoot<TEntityId>
    where TEntityId : EntityId
{
    private readonly ApplicationDbContext _context;
    private DbSet<TEntity> DbSet => _context.Set<TEntity>();
    private readonly Func<TEntityId, Expression<Func<TEntity, bool>>> PredicateById = id => e => e.Id.Equals(id);

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }


    #region Basic

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken? cancellationToken = null)
    {
        var entityEntry = await DbSet.AddAsync(entity, cancellationToken ?? CancellationToken.None);
        await SaveChangesAsync(cancellationToken);
        ClearChangeTracker();
        return entityEntry.Entity;
    }

    public async Task<TEntity?> GetByIdAsync(
            TEntityId id,
            CancellationToken? cancellationToken = null
        ) => await DbSet
        .AsNoTracking()
        .FirstOrDefaultAsync(PredicateById(id), cancellationToken ?? CancellationToken.None);

    public async Task<List<TEntity>> GetAllAsync(CancellationToken? cancellationToken = null)
        => await DbSet.AsNoTracking().ToListAsync();

    public Task<bool> ExistByIdAsync(TEntityId id, CancellationToken? cancellationToken = null) => DbSet
        .AsNoTracking()
        .AnyAsync(PredicateById(id), cancellationToken ?? CancellationToken.None);

    public Task<int> CountAsync(CancellationToken? cancellationToken = null)
        => DbSet.AsNoTracking().CountAsync(cancellationToken ?? CancellationToken.None);

    public async Task<TEntity> UpdateAsync(
            TEntity entity,
            CancellationToken? cancellationToken = null
        )
    {
        entity = DbSet.Update(entity).Entity;
        await SaveChangesAsync(cancellationToken);
        ClearChangeTracker();
        return entity;
    }

    public async Task<bool> DeleteByIdAsync(
            TEntityId id,
            CancellationToken? cancellationToken = null
        )
    {
        var deleted =
            await DbSet
                .Where(PredicateById(id))
                .ExecuteDeleteAsync(cancellationToken ?? CancellationToken.None)
            > 0;
        await SaveChangesAsync(cancellationToken);
        ClearChangeTracker();
        return deleted;
    }

    #endregion


    #region Specification

    public async Task<TEntity?> GetOneAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>
    {
        var entity = await DbSet
            .AsNoTracking()
            .AddSpecification(specification)
            .FirstOrDefaultAsync(cancellationToken ?? CancellationToken.None);
        return entity;
    }

    public async Task<List<TEntity>> GetManyAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity> => await DbSet
        .AsNoTracking()
        .AddSpecification(specification)
        .ToListAsync(cancellationToken ?? CancellationToken.None);

    public async Task<int> CountAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity> => await DbSet
        .AsNoTracking()
        .AddSpecification(specification)
        .CountAsync(cancellationToken ?? CancellationToken.None);

    public async Task<bool> DeleteAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>
    {
        var deleted =
            await DbSet
                .AddSpecification(specification)
                .ExecuteDeleteAsync(cancellationToken ?? CancellationToken.None)
            > 0;
        await SaveChangesAsync(cancellationToken);
        ClearChangeTracker();
        return deleted;
    }

    public async Task<bool> ExistAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity> => await DbSet
        .AsNoTracking()
        .AddSpecification(specification)
        .AnyAsync(cancellationToken ?? CancellationToken.None);

    #endregion


    #region Selector

    public async Task<TSelector?> GetByIdAsync<TSelector>(
            TEntityId id,
            Expression<Func<TEntity, TSelector>> selector,
            CancellationToken? cancellationToken = null
        )
        where TSelector : Selector<TEntity, TSelector> => await DbSet
        .AsNoTracking()
        .Where(PredicateById(id))
        .Select(selector)
        .SingleOrDefaultAsync(cancellationToken ?? CancellationToken.None);

    public async Task<TSelector?> GetOneAsync<TSpecification, TSelector>(
            TSpecification specification,
            Expression<Func<TEntity, TSelector>> selector,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>
        where TSelector : Selector<TEntity, TSelector> => await DbSet
        .AsNoTracking()
        .AddSpecification(specification)
        .Select(selector)
        .SingleOrDefaultAsync(cancellationToken ?? CancellationToken.None);

    public async Task<List<TSelector>> GetManyAsync<TSelector>(
            Expression<Func<TEntity, TSelector>> selector,
            CancellationToken? cancellationToken = null
        )
        where TSelector : Selector<TEntity, TSelector> => await DbSet
        .AsNoTracking()
        .Select(selector)
        .ToListAsync(cancellationToken ?? CancellationToken.None);

    public async Task<List<TSelector>> GetManyAsync<TSpecification, TSelector>(
            TSpecification specification,
            Expression<Func<TEntity, TSelector>> selector,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>
        where TSelector : Selector<TEntity, TSelector> => await DbSet
        .AsNoTracking()
        .AddSpecification(specification)
        .Select(selector)
        .ToListAsync(cancellationToken ?? CancellationToken.None);

    #endregion


    private async Task SaveChangesAsync(CancellationToken? cancellationToken = null)
    {
        await _context.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
    }

    private void ClearChangeTracker()
    {
        _context.ChangeTracker.Clear();
    }
}
