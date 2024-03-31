using System.Reflection;
using SpringMountain.Modularity.Abstraction;

namespace SpringMountain.Modularity;

/// <summary>
/// 核心模块基类（抽象类）
/// </summary>
public abstract class CoreModuleBase : ICoreModule
{
    /// <summary>
    /// 配置服务容器之前
    /// </summary>
    /// <param name="context"></param>
    public virtual void PreConfigureServices(ServiceCollectionContext context)
    {
    }

    /// <summary>
    /// 配置服务容器
    /// </summary>
    /// <param name="context"></param>
    public virtual void ConfigureServices(ServiceCollectionContext context)
    {
    }

    /// <summary>
    /// 配置服务容器之后
    /// </summary>
    /// <param name="context"></param>
    public virtual void PostConfigureServices(ServiceCollectionContext context)
    {
    }

    /// <summary>
    /// 配置中间件之前
    /// </summary>
    /// <param name="context"></param>
    public virtual void PreConfigure(ApplicationBuilderContext context)
    {
    }

    /// <summary>
    /// 配置中间件
    /// </summary>
    /// <param name="context"></param>
    public virtual void Configure(ApplicationBuilderContext context)
    {
    }

    /// <summary>
    /// 配置中间件之后
    /// </summary>
    /// <param name="context"></param>
    public virtual void PostConfigure(ApplicationBuilderContext context)
    {
    }

    /// <summary>
    /// 关闭应用
    /// </summary>
    /// <param name="context"></param>
    public virtual void Shutdown(ShutdownApplicationContext context)
    {
    }

    /// <summary>
    /// 判断类型是否是核心模块类。
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsCoreModule(Type type)
    {
        var typeInfo = type.GetTypeInfo();
        return
            typeInfo.IsClass &&
            !typeInfo.IsAbstract &&
            !typeInfo.IsGenericType &&
            typeof(ICoreModule).GetTypeInfo().IsAssignableFrom(type);
    }

    /// <summary>
    /// 检验模块类型是否是核心模块类。
    /// </summary>
    /// <param name="moduleType"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void CheckCoreModuleType(Type moduleType)
    {
        if (!IsCoreModule(moduleType))
        {
            throw new ArgumentException("Given type is not a Core module: " + moduleType.AssemblyQualifiedName);
        }
    }
}