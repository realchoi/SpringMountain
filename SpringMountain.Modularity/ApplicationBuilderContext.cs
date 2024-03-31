using Microsoft.AspNetCore.Builder;

namespace SpringMountain.Modularity;

public class ApplicationBuilderContext
{
    public IApplicationBuilder ApplicationBuilder { get; set; }

    public ApplicationBuilderContext(IApplicationBuilder applicationBuilder)
    {
        ApplicationBuilder = applicationBuilder ?? throw new ArgumentNullException(nameof(applicationBuilder));
    }
}