using Microsoft.EntityFrameworkCore;

namespace SpringMountain.Framework.Uow.EntityFrameworkCore;

/// <summary>
/// 数据库上下文对象提供器接口
/// </summary>
/// <typeparam name="TDbContext"></typeparam>
public interface IDbContextProvider<TDbContext>
    where TDbContext : DbContext
{
    /// <summary>
    /// 获取数据上下文对象
    /// </summary>
    /// <returns></returns>
    Task<TDbContext> GetDbContextAsync();
}