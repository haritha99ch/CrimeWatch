using CrimeWatch.Infrastructure.Common;

namespace CrimeWatch.Infrastructure.Contracts.Repositories;
public interface IRepository<TEntity, TValueObject>
    where TEntity : Entity<TValueObject> where TValueObject : ValueObject
{


    # region Common

    Task<TEntity> AddAsync(TEntity entity, CancellationToken? cancellationToken = null);
    Task<TEntity?> GetByIdAsync(TValueObject id, CancellationToken? cancellationToken = null);
    Task<List<TEntity>> GetAllAsync(CancellationToken? cancellationToken = null);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken? cancellationToken = null);
    Task<bool> DeleteByIdAsync(TValueObject id, CancellationToken? cancellationToken = null);
    Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken? cancellationToken = null);
    Task<bool> ExistsByIdAsync(TValueObject id, CancellationToken? cancellationToken = null);
    Task<int> CountAsync(CancellationToken? cancellationToken = null);

    #endregion


    #region Specification

    Task<TEntity?> GetByIdAsync<TSpecification>(TValueObject id,
        CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>;

    Task<List<TEntity>> GetAllAsync<TSpecification>(
        CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>;

    Task<TEntity?> GetOneAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>;

    Task<List<TEntity>> GetManyAsync<TSpecification>(TSpecification specification,
        CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>;


    Task<bool> ExistsAsync<TSpecification>(TSpecification specification, CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>;

    Task<int> CountByAsync<TSpecification>(TSpecification specification, CancellationToken? cancellationToken = null)
        where TSpecification : Specification<TEntity, TValueObject>;

    #endregion


    #region Selector

    Task<TResult?> GetByIdAsync<TResult>(TValueObject id, Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null);

    Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null);

    Task<TResult?> GetOneAsync<TResult, TSpecification>(TSpecification specification,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity, TValueObject>;

    Task<List<TResult>> GetManyAsync<TResult, TSpecification>(TSpecification specification,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken? cancellationToken = null) where TSpecification : Specification<TEntity, TValueObject>;

    #endregion


    IRepository<TEntity, TValueObject> AsTracking();
}
