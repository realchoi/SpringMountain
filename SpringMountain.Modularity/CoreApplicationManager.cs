using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SpringMountain.Modularity.Abstraction;

namespace SpringMountain.Modularity;

/// <summary>
/// 核心应用管理器
/// </summary>
public class CoreApplicationManager : ICoreApplicationManager
{
    /// <summary>
    /// Startup 模块类型
    /// </summary>
    public Type StartupModuleType { get; }

    public IReadOnlyList<ICoreModuleDescriptor> Modules { get; }

    /// <summary>
    /// 初始化核心应用管理器对象。
    /// </summary>
    /// <param name="startupModuleType">Startup 模块类型</param>
    /// <param name="services">服务容器</param>
    /// <exception cref="ArgumentNullException"></exception>
    public CoreApplicationManager(Type startupModuleType, IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        StartupModuleType = startupModuleType ?? throw new ArgumentNullException(nameof(startupModuleType));

        services.TryAddSingleton<ICoreApplicationManager>(this);
        services.TryAddSingleton<IModuleLoader>(new ModuleLoader());
        Modules = LoadModules(services);
        ConfigureConfigureServices(services);
    }

    /// <summary>
    /// 配置服务容器。
    /// </summary>
    /// <remarks>
    /// 内部逻辑顺序：1. 配置服务容器之前；2. 配置服务容器；3. 配置服务容器之后。
    /// </remarks>
    /// <param name="services">服务容器</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void ConfigureConfigureServices(IServiceCollection services)
    {
        var context = new ServiceCollectionContext(services);
        foreach (var moduleDescriptor in Modules)
        {
            try
            {
                moduleDescriptor.Instance.PreConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An error occurred during PreConfigureServices phase of the module " +
                    moduleDescriptor.ModuleType.AssemblyQualifiedName + ". See the inner exception for details.", ex);
            }
        }

        foreach (var moduleDescriptor in Modules)
        {
            try
            {
                moduleDescriptor.Instance.ConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An error occurred during ConfigureServices phase of the module " +
                    moduleDescriptor.ModuleType.AssemblyQualifiedName + ". See the inner exception for details.", ex);
            }
        }

        foreach (var moduleDescriptor in Modules)
        {
            try
            {
                moduleDescriptor.Instance.PostConfigureServices(context);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An error occurred during PostConfigureServices phase of the module " +
                    moduleDescriptor.ModuleType.AssemblyQualifiedName + ". See the inner exception for details.", ex);
            }
        }
    }

    /// <summary>
    /// 配置请求管道。
    /// </summary>
    /// <remarks>
    /// 内部逻辑顺序：1. 配置请求管道之前；2. 配置请求管道；3. 配置请求管道之后。
    /// </remarks>
    /// <param name="app">请求管道构建器</param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Configure(IApplicationBuilder app)
    {
        var context = new ApplicationBuilderContext(app);
        foreach (var moduleDescriptor in Modules)
        {
            try
            {
                moduleDescriptor.Instance.PreConfigure(context);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An error occurred during PreConfigure phase of the module " +
                    moduleDescriptor.ModuleType.AssemblyQualifiedName + ". See the inner exception for details.", ex);
            }
        }

        foreach (var moduleDescriptor in Modules)
        {
            try
            {
                moduleDescriptor.Instance.Configure(context);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An error occurred during Configure phase of the module " +
                    moduleDescriptor.ModuleType.AssemblyQualifiedName + ". See the inner exception for details.", ex);
            }
        }

        foreach (var moduleDescriptor in Modules)
        {
            try
            {
                moduleDescriptor.Instance.PostConfigure(context);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An error occurred during PostConfigure phase of the module " +
                    moduleDescriptor.ModuleType.AssemblyQualifiedName + ". See the inner exception for details.", ex);
            }
        }
    }

    /// <summary>
    /// 关闭应用。
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Shutdown(IServiceProvider serviceProvider)
    {
        var context = new ShutdownApplicationContext(serviceProvider);
        var modules = Modules.Reverse().ToList();
        foreach (var moduleDescriptor in modules)
        {
            try
            {
                moduleDescriptor.Instance.Shutdown(context);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "An error occurred during Shutdown phase of the module  " +
                    moduleDescriptor.ModuleType.AssemblyQualifiedName + ". See the inner exception for details.", ex);
            }
        }
    }

    /// <summary>
    /// 加载模块
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <returns></returns>
    private IReadOnlyList<ICoreModuleDescriptor> LoadModules(
        IServiceCollection services)
    {
        var moduleLoader = (IModuleLoader)services.FirstOrDefault(p => p.ServiceType == typeof(IModuleLoader))
            ?.ImplementationInstance;
        return moduleLoader?.LoadModules(services, StartupModuleType);
    }
}