using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SpringMountain.Modularity;

public class ServiceCollectionContext
{
    public IServiceCollection Services { get; }

    public IDictionary<string, object> Items { get; }

    public IConfiguration Configuration { get; }

    public object this[string key]
    {
        get => Items[key];
        set => Items[key] = value;
    }

    public ServiceCollectionContext(IServiceCollection services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
        Items = new Dictionary<string, object>();
        Configuration = services.BuildServiceProvider().GetService<IConfiguration>();
    }
}