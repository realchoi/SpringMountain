using SpringMountain.Framework.Core.Domain.Entities;

namespace SpringMountain.Framework.Core.Domain.Repositories;

/// <summary>
/// 仓储接口
/// </summary>
public interface IRepository
{
}

/// <summary>
/// 仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类</typeparam>
public interface IRepository<TEntity> : IRepository where TEntity : class, IEntity
{
    /// <summary>
    /// 获取 IQueryable
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetQueryable();

    /// <summary>
    /// 新增实体
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增实体集合
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除实体
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 删除实体集合
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <returns></returns>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新实体集合
    /// </summary>
    /// <returns></returns>
    Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 重新加载
    /// </summary>
    /// <returns></returns>
    Task ReloadAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 执行 SQL
    /// </summary>
    /// <returns></returns>
    Task<IQueryable<TEntity>> FromSqlRaw(string sql, params object[] parameters);
}

/// <summary>
/// 仓储接口，指定主键
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IRepository<TEntity, in TKey> : IRepository<TEntity>
    where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// 根据主键获取，当获取失败时抛出异常
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> Get(TKey key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据主键获取，当获取失败时返回 null
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> GetOrDefault(TKey key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据主键删除
    /// </summary>
    /// <returns></returns>
    Task RemoveAsync(TKey key, CancellationToken cancellationToken = default);
}