using Microsoft.Extensions.DependencyInjection;

namespace SpringMountain.Infrastructure.Snowflake;

public static class SnowflakeDependencyInjection
{
    /// <summary>
    /// 注入雪花算法服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="option"></param>
    /// <returns></returns>
    public static IServiceCollection AddSnowflake(this IServiceCollection services, Action<SnowflakeOption> option)
    {
        services.Configure(option);
        services.AddSingleton<ISnowflakeIdMaker, SnowflakeIdMaker>();
        return services;
    }
}