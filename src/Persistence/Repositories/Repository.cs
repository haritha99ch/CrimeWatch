using Domain.Common.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Persistence.Common.Specifications;
using Persistence.Common.Specifications.Helpers;
using Persistence.Contracts.Repositories;
using Persistence.Contracts.Selectors;
using System.Linq.Expressions;

namespace Persistence.Repositories;
internal class Repository<TEntity, TEntityId> : IRepository<TEntity, TEntityId>
    where TEntity : AggregateRoot<TEntityId>
    where TEntityId : EntityId
{
    private readonly ApplicationDbContext _context;
    private DbSet<TEntity> DbSet => _context.Set<TEntity>();
    private readonly Func<TEntityId, Expression<Func<TEntity, bool>>> _predicateById = id => e => e.Id.Equals(id);

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
        ) =>
        await DbSet.AsNoTracking()
            .FirstOrDefaultAsync(_predicateById(id), cancellationToken ?? CancellationToken.None);

    public async Task<TEntity?> GetByIdAsTrackingAsync(
            TEntityId id,
            CancellationToken? cancellationToken = null
        ) =>
        await DbSet.FirstOrDefaultAsync(_predicateById(id), cancellationToken ?? CancellationToken.None);

    public async Task<List<TEntity>> GetAllAsync(CancellationToken? cancellationToken = null)
        => await DbSet.AsNoTracking().ToListAsync();

    public async Task<bool> ExistByIdAsync(TEntityId id, CancellationToken? cancellationToken = null) =>
        await DbSet.AsNoTracking()
            .AnyAsync(_predicateById(id), cancellationToken ?? CancellationToken.None);

    public async Task<int> CountAsync(CancellationToken? cancellationToken = null)
        => await DbSet.AsNoTracking().CountAsync(cancellationToken ?? CancellationToken.None);

    public async Task<TEntity> UpdateAsync(
            TEntity entity,
            CancellationToken? cancellationToken = null
        )
    {
        DbSet.Update(entity);
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
            await DbSet.Where(_predicateById(id))
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

    public async Task<TResult?> GetOneAsync<TSpecification, TResult>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TResult : ISelector
        where TSpecification : Specification<TEntity, TResult>
    {
        var queryResult = DbSet.AsNoTracking().AddSpecification(specification);
        IQueryable<TResult> queryable = default!;
        queryResult.Handle(e => { queryable = e; });
        return await queryable.FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> GetManyAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity> =>
        await DbSet.AsNoTracking()
            .AddSpecification(specification)
            .ToListAsync(cancellationToken ?? CancellationToken.None);

    public async Task<List<TResult>> GetManyAsync<TSpecification, TResult>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        ) where TSpecification : Specification<TEntity, TResult>
    {
        var queryResult = DbSet.AsNoTracking().AddSpecification(specification);
        IQueryable<TResult>? queryable = default;
        IQueryable<List<TResult>>? listQueryable = default;
        queryResult.Handle(e => { queryable = e; },
            e => { listQueryable = e; });

        return queryable is not null
            ? await queryable.ToListAsync()
            : await listQueryable!.FirstOrDefaultAsync() ?? [];
    }

    public async Task<int> CountAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity> =>
        await DbSet.AsNoTracking()
            .AddSpecification(specification)
            .CountAsync(cancellationToken ?? CancellationToken.None);

    public async Task<bool> DeleteAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>
    {
        var deleted = await DbSet.AddSpecification(specification)
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
        where TSpecification : Specification<TEntity> =>
        await DbSet.AsNoTracking()
            .AddSpecification(specification)
            .AnyAsync(cancellationToken ?? CancellationToken.None);

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
