using Domain.Common.Models;
using Persistence.Common.Specifications;
using Shared.Contracts.Selectors;

namespace Persistence.Contracts.Repositories;
public interface IRepository<TEntity, in TEntityId>
    where TEntity : AggregateRoot<TEntityId>
    where TEntityId : EntityId
{


    #region Basic

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistByIdAsync(TEntityId id, CancellationToken cancellationToken = default);
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(TEntityId id, CancellationToken cancellationToken = default);

    #endregion


    #region Specification

    Task<TEntity?> GetOneAsync<TSpecification>(
            TSpecification specification,
            CancellationToken cancellationToken = default
        )
        where TSpecification : Specification<TEntity>;

    Task<TResult?> GetOneAsync<TSpecification, TResult>(
            TSpecification specification,
            CancellationToken cancellationToken = default
        )
        where TSpecification : Specification<TEntity, TResult>
        where TResult : ISelector;

    Task<List<TEntity>> GetManyAsync<TSpecification>(
            TSpecification specification,
            CancellationToken cancellationToken = default
        )
        where TSpecification : Specification<TEntity>;

    Task<List<TResult>> GetManyAsync<TSpecification, TResult>(
            TSpecification specification,
            CancellationToken cancellationToken = default
        )
        where TSpecification : Specification<TEntity, TResult>
        where TResult : ISelector;

    Task<bool> ExistAsync<TSpecification>(
            TSpecification specification,
            CancellationToken cancellationToken = default
        )
        where TSpecification : Specification<TEntity>;

    Task<int> CountAsync<TSpecification>(
            TSpecification specification,
            CancellationToken cancellationToken = default
        )
        where TSpecification : Specification<TEntity>;

    Task<bool> DeleteAsync<TSpecification>(
            TSpecification specification,
            CancellationToken cancellationToken = default
        )
        where TSpecification : Specification<TEntity>;

    #endregion


    IRepository<TEntity, TEntityId> AsTracking();
}
