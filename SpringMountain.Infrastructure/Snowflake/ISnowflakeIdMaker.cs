namespace SpringMountain.Infrastructure.Snowflake;

/// <summary>
/// 雪花算法接口
/// </summary>
public interface ISnowflakeIdMaker
{
    /// <summary>
    /// 获取 id
    /// </summary>
    /// <param name="workId"></param>
    /// <returns></returns>
    long NextId(int? workId = null);
}