using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpringMountain.Modularity.Abstraction;

namespace SpringMountain.Modularity;

/// <summary>
/// 请求管道构建器扩展方法
/// </summary>
public static class CoreFrameworkApplicationBuilderExtensions
{
    /// <summary>
    /// 创建请求管道构建器对象。
    /// </summary>
    /// <param name="app">请求管道构建器</param>
    public static void BuildApplicationBuilder(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        // 配置请求管道
        var application = app.ApplicationServices.GetRequiredService<ICoreApplicationManager>();
        application.Configure(app);

        // 注册应用关闭事件
        var requiredService = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
        requiredService.ApplicationStopping.Register(() => application.Shutdown(app.ApplicationServices));
    }
}