using Domain.Common.Models;
using Persistence.Common.Specifications;
using Persistence.Contracts.Selectors;

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

    Task<TResult?> GetOneAsync<TSpecification, TResult>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TResult : ISelector
        where TSpecification : Specification<TEntity, TResult>;

    Task<List<TEntity>> GetManyAsync<TSpecification>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        )
        where TSpecification : Specification<TEntity>;

    Task<List<TResult>> GetManyAsync<TSpecification, TResult>(
            TSpecification specification,
            CancellationToken? cancellationToken = null
        ) where TSpecification : Specification<TEntity, TResult>;

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


}
