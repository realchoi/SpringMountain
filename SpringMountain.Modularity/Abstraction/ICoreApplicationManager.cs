using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SpringMountain.Modularity.Abstraction;

/// <summary>
/// 应用管理器接口
/// </summary>
public interface ICoreApplicationManager
{
    void ConfigureConfigureServices(IServiceCollection services);

    void Configure(IApplicationBuilder app);

    void Shutdown(IServiceProvider serviceProvider);
}