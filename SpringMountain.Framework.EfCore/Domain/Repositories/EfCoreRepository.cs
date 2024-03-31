using Microsoft.EntityFrameworkCore;
using SpringMountain.Api.Exceptions.Contracts;
using SpringMountain.Framework.Core.Domain.Entities;
using SpringMountain.Framework.Core.Domain.Repositories;
using SpringMountain.Framework.Uow.EntityFrameworkCore;

namespace SpringMountain.Framework.EfCore.Domain.Repositories;

/// <summary>
/// EfCore 仓储类（指定数据库上下文类型、实体类型）
/// </summary>
/// <typeparam name="TDbContext">数据库上下文类型</typeparam>
/// <typeparam name="TEntity">实体类型</typeparam>
public class EfCoreRepository<TDbContext, TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
    where TDbContext : CoreDbContext
{
    /// <summary>
    /// 数据库上下文对象提供器
    /// </summary>
    private readonly IDbContextProvider<TDbContext> _dbContextProvider;

    /// <summary>
    /// 数据库上下文对象
    /// </summary>
    protected TDbContext DbContext => _dbContextProvider.GetDbContextAsync().Result;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dbContextProvider">数据库上下文对象提供器</param>
    public EfCoreRepository(IDbContextProvider<TDbContext> dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    /// <summary>
    /// 获取 IQueryable
    /// </summary>
    /// <returns></returns>
    public IQueryable<TEntity> GetQueryable()
    {
        var context = _dbContextProvider.GetDbContextAsync().Result;
        return context.Set<TEntity>().AsQueryable();
    }

    /// <summary>
    /// 新增实体
    /// </summary>
    /// <returns></returns>
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    /// <summary>
    /// 新增实体集合
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task AddAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        await context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    /// <summary>
    /// 删除实体
    /// </summary>
    /// <returns></returns>
    public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        context.Remove(entity);
    }

    /// <summary>
    /// 删除实体集合
    /// </summary>
    /// <returns></returns>
    public async Task RemoveAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        context.RemoveRange(entities);
    }

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <returns></returns>
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        context.Update(entity);
    }

    /// <summary>
    /// 更新实体集合
    /// </summary>
    /// <returns></returns>
    public async Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        context.UpdateRange(entities);
    }

    /// <summary>
    /// 重新加载
    /// </summary>
    /// <returns></returns>
    public async Task ReloadAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        await context.Entry(entity).ReloadAsync(cancellationToken);
    }

    /// <summary>
    /// 执行 SQL
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public async Task<IQueryable<TEntity>> FromSqlRaw(string sql, params object[] parameters)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        return context.Set<TEntity>().FromSqlRaw(sql, parameters);
    }
}

/// <summary>
/// EfCore 仓储类（指定数据库上下文类型、实体类型、实体的主键类型）
/// </summary>
/// <typeparam name="TDbContext">数据库上下文类型</typeparam>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TKey">实体的主键类型</typeparam>
public class EfCoreRepository<TDbContext, TEntity, TKey> : EfCoreRepository<TDbContext, TEntity>,
    IRepository<TEntity, TKey>
    where TDbContext : CoreDbContext
    where TEntity : class, IEntity<TKey>
{
    /// <summary>
    /// 数据库上下文对象提供器
    /// </summary>
    private readonly IDbContextProvider<TDbContext> _dbContextProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public EfCoreRepository(IDbContextProvider<TDbContext> dbContextProvider) : base(dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    /// <summary>
    /// 根据主键获取，当获取失败时抛出异常
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TEntity> Get(TKey key, CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        var entity = await context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id.Equals(key), cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException("对象不存在");
        }

        return entity;
    }

    /// <summary>
    /// 根据主键获取，当获取失败时返回 null
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TEntity> GetOrDefault(TKey key, CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        var entity = await context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id.Equals(key), cancellationToken);
        return entity;
    }

    /// <summary>
    /// 根据主键删除
    /// </summary>
    /// <returns></returns>
    public async Task RemoveAsync(TKey key, CancellationToken cancellationToken = default)
    {
        var context = await _dbContextProvider.GetDbContextAsync();
        var entity = await context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id.Equals(key), cancellationToken);
        if (entity != null)
        {
            context.Remove(entity);
        }
    }
}