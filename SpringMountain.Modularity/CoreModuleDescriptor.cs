using SpringMountain.Modularity.Abstraction;

namespace SpringMountain.Modularity;

public class CoreModuleDescriptor : ICoreModuleDescriptor
{
    /// <summary>
    /// 模块类型
    /// </summary>
    public Type ModuleType { get; }

    /// <summary>
    /// 核心模块对象实例
    /// </summary>
    public ICoreModule Instance { get; }

    public IReadOnlyList<ICoreModuleDescriptor> Dependencies => _dependencies;

    private readonly List<ICoreModuleDescriptor> _dependencies;

    public CoreModuleDescriptor(Type moduleType, ICoreModule instance)
    {
        ModuleType = moduleType ?? throw new ArgumentNullException(nameof(moduleType));
        Instance = instance ?? throw new ArgumentNullException(nameof(instance));
        _dependencies = new List<ICoreModuleDescriptor>();
    }

    public void SetDependencies(List<CoreModuleDescriptor> modules)
    {
        foreach (var dependedModuleType in CoreModuleHelper.FindDependedModuleTypes(ModuleType))
        {
            var dependedModule = modules.FirstOrDefault(m => m.ModuleType == dependedModuleType);
            if (dependedModule == null)
            {
                throw new InvalidOperationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + ModuleType.AssemblyQualifiedName);
            }
            _dependencies.Add(dependedModule);
        }
    }
}