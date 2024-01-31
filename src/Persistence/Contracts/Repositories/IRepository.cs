using Domain.Common.Models;
using Persistence.Common.Selectors;
using Persistence.Common.Specifications;
using System.Linq.Expressions;

namespace Persistence.Contracts.Repositories;
public interface IRepository<TEntity, TEntityId>
    where TEntity : AggregateRoot<TEntityId>
    where TEntityId : EntityId
{


    #region Basic

    Task<TEntity> AddAsync(TEntity entity, CancellationToken? cancellationToken = null);
    Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken? cancellationToken = null);
    Task<List<TEntity>> GetAllAsync(CancellationToken? cancellationToken = null);
    Task<bool> ExistByIdAsync(TEntityId id, CancellationToken? cancellationToken = null);
    Task<int> CountAsync(CancellationToken? cancellationToken = null);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken? cancellationToken = null);
    Task<bool> DeleteByIdAsync(TEntityId id, CancellationToken? cancellationToken = null);

    #endregion


    #region Specification

    Task<TEntity?> GetOneAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>;

    Task<List<TEntity>> GetManyAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>;

    Task<bool> ExistAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>;

    Task<int> CountAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>;

    Task<bool> DeleteAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>;

    #endregion


    #region Selector

    Task<TSelector?> GetByIdAsync<TSelector>(
            TEntityId id,
            Expression<Func<TEntity, TSelector>> selector,
            CancellationToken? cancellationToken = null
        )
        where TSelector : Selector<TEntity, TSelector>;

    Task<List<TSelector>> GetByIdAsync<TSelector>(
            TEntityId id,
            Expression<Func<TEntity, List<TSelector>>> selector,
            CancellationToken? cancellationToken = null
        )
        where TSelector : Selector<TEntity, TSelector>;

    Task<List<TSelector>> GetManyAsync<TSelector>(
            Expression<Func<TEntity, TSelector>> selector,
            CancellationToken? cancellationToken = null
        )
        where TSelector : Selector<TEntity, TSelector>;

    Task<TSelector?> GetOneAsync<TSpecification, TSelector>(
            TSpecification specification,
            Expression<Func<TEntity, TSelector>> selector,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>
        where TSelector : Selector<TEntity, TSelector>;

    Task<List<TSelector>> GetOneAsync<TSpecification, TSelector>(
            TSpecification specification,
            Expression<Func<TEntity, List<TSelector>>> selector,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>
        where TSelector : Selector<TEntity, TSelector>;

    Task<List<TSelector>> GetManyAsync<TSpecification, TSelector>(
            TSpecification specification,
            Expression<Func<TEntity, TSelector>> selector,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>
        where TSelector : Selector<TEntity, TSelector>;

    #endregion


}
