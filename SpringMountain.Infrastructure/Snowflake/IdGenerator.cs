using Microsoft.Extensions.DependencyInjection;

namespace SpringMountain.Infrastructure.Snowflake;

/// <summary>
/// ID 生成器
/// </summary>
public static class IdGenerator
{
    private static IServiceProvider _serviceProvider;

    public static void SetServiceProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 生成新的 ID
    /// </summary>
    /// <param name="workId">工作机器 id</param>
    /// <returns></returns>
    public static long NextId(int? workId = null)
    {
        if (_serviceProvider == null)
        {
            throw new ArgumentNullException(nameof(_serviceProvider));
        }

        var snowflakeIdMaker = _serviceProvider.GetRequiredService<ISnowflakeIdMaker>();
        if (snowflakeIdMaker == null)
        {
            throw new ArgumentNullException(nameof(snowflakeIdMaker));
        }

        return snowflakeIdMaker.NextId(workId);
    }
}