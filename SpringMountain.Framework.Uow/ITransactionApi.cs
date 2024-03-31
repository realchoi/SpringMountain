namespace SpringMountain.Framework.Uow;

/// <summary>
/// 事务相关方法接口
/// </summary>
public interface ITransactionApi : IDisposable
{
    /// <summary>
    /// 提交事务
    /// </summary>
    /// <returns></returns>
    Task CommitAsync();
}