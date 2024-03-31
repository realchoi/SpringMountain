using Microsoft.Extensions.DependencyInjection;
using SpringMountain.Modularity.Abstraction;

namespace SpringMountain.Modularity;

/// <summary>
/// 服务容器扩展方法
/// </summary>
public static class CoreFrameworkServiceCollectionExtensions
{
    /// <summary>
    /// 配置服务容器。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="services"></param>
    public static void ConfigureServiceCollection<T>(this IServiceCollection services)
        where T : ICoreModule
    {
        ConfigureServiceCollection(services, typeof(T));
    }

    /// <summary>
    /// 配置服务容器。
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <param name="startupModuleType">Startup 模块类</param>
    public static void ConfigureServiceCollection(this IServiceCollection services, Type startupModuleType)
    {
        CoreApplicationManagerFactory.CreateCoreApplication(startupModuleType, services);
    }
}