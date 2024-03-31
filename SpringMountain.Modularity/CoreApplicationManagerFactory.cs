using Microsoft.Extensions.DependencyInjection;
using SpringMountain.Modularity.Abstraction;

namespace SpringMountain.Modularity;

/// <summary>
/// 核心应用管理器工厂
/// </summary>
public static class CoreApplicationManagerFactory
{
    /// <summary>
    /// 创建核心应用
    /// </summary>
    /// <param name="startupModuleType">Startup 模块类型</param>
    /// <param name="services">服务容器</param>
    /// <returns></returns>
    public static ICoreApplicationManager CreateCoreApplication(Type startupModuleType, IServiceCollection services)
    {
        return new CoreApplicationManager(startupModuleType, services);
    }
}