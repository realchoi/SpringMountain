using Microsoft.Extensions.DependencyInjection;

namespace SpringMountain.Modularity.Abstraction;

public interface IModuleLoader
{
    /// <summary>
    /// 加载模块
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <param name="startupModuleType">启动模块类型</param>
    /// <returns></returns>
    ICoreModuleDescriptor[] LoadModules(
        IServiceCollection services,
        Type startupModuleType);
}