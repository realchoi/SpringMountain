namespace SpringMountain.Modularity.Abstraction;

public interface ICoreModuleDescriptor
{
    /// <summary>
    /// 模块类型
    /// </summary>
    Type ModuleType { get; }

    /// <summary>
    /// 核心模块对象实例
    /// </summary>
    ICoreModule Instance { get; }

    IReadOnlyList<ICoreModuleDescriptor> Dependencies { get; }
}