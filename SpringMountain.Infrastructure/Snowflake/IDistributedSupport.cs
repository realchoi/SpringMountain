namespace SpringMountain.Infrastructure.Snowflake;

public interface IDistributedSupport
{
    /// <summary>
    /// 获取下一个可用的机器 id
    /// </summary>
    /// <returns></returns>
    Task<int> GetNextWorkId();

    /// <summary>
    /// 刷新机器 id 的存活状态
    /// </summary>
    /// <returns></returns>
    Task RefreshAlive();
}