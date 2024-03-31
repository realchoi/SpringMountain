using System.Data;

namespace SpringMountain.Framework.Uow;

/// <summary>
/// 工作单元配置项
/// </summary>
public class UnitOfWorkOptions
{
    /// <summary>
    /// 是否使用事务，默认 false
    /// </summary>
    public bool IsTransactional { get; set; }

    /// <summary>
    /// 事务隔离级别
    /// </summary>
    public IsolationLevel? IsolationLevel { get; set; }

    /// <summary>
    /// 超时时间，单位：毫秒
    /// </summary>
    public int? Timeout { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public UnitOfWorkOptions()
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="isTranscational">是否使用事务</param>
    /// <param name="isolationLevel">事务隔离级别</param>
    /// <param name="timeout">超时时间，单位：毫秒</param>
    public UnitOfWorkOptions(bool isTranscational = false, IsolationLevel? isolationLevel = null, int? timeout = null)
    {
        IsTransactional = isTranscational;
        IsolationLevel = isolationLevel;
        Timeout = timeout;
    }

    /// <summary>
    /// 生成新的 UnitOfWorkOptions 对象
    /// </summary>
    /// <returns></returns>
    public UnitOfWorkOptions Clone()
    {
        return new UnitOfWorkOptions
        {
            IsTransactional = IsTransactional,
            IsolationLevel = IsolationLevel,
            Timeout = Timeout
        };
    }
}