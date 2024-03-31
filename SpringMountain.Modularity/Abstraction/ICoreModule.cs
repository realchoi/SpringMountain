namespace SpringMountain.Modularity.Abstraction;

/// <summary>
/// 核心模块接口
/// </summary>
public interface ICoreModule
{
    /// <summary>
    /// 配置服务容器之前
    /// </summary>
    /// <param name="context"></param>
    void PreConfigureServices(ServiceCollectionContext context);

    /// <summary>
    /// 配置服务容器
    /// </summary>
    /// <param name="context"></param>
    void ConfigureServices(ServiceCollectionContext context);

    /// <summary>
    /// 配置服务容器之后
    /// </summary>
    /// <param name="context"></param>
    void PostConfigureServices(ServiceCollectionContext context);

    /// <summary>
    /// 配置中间件之前
    /// </summary>
    /// <param name="context"></param>
    void PreConfigure(ApplicationBuilderContext context);

    /// <summary>
    /// 配置中间件
    /// </summary>
    /// <param name="app"></param>
    void Configure(ApplicationBuilderContext app);

    /// <summary>
    /// 配置中间件之后
    /// </summary>
    /// <param name="context"></param>
    void PostConfigure(ApplicationBuilderContext context);

    /// <summary>
    /// 关闭应用
    /// </summary>
    /// <param name="context"></param>
    void Shutdown(ShutdownApplicationContext context);
}