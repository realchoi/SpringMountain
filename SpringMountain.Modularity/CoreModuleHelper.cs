using System.Reflection;
using SpringMountain.Modularity.Attribute;

namespace SpringMountain.Modularity;

public class CoreModuleHelper
{
    public static List<Type> FindAllModuleTypes(Type startupModuleType)
    {
        var moduleTypes = new List<Type>();
        AddModules(moduleTypes, startupModuleType);
        return moduleTypes;
    }

    /// <summary>
    /// 查找给定的模块类型所依赖的类型。
    /// </summary>
    /// <param name="moduleType">给定的模块类型</param>
    /// <returns></returns>
    public static List<Type> FindDependedModuleTypes(Type moduleType)
    {
        CoreModuleBase.CheckCoreModuleType(moduleType);
        var source = new List<Type>();
        foreach (var dependedTypes in moduleType.GetCustomAttributes().OfType<DependsOnAttribute>())
        {
            foreach (var dependedType in dependedTypes.GetDependedTypes())
            {
                if (!source.Contains(dependedType))
                    source.Add(dependedType);
            }
        }

        return source;
    }

    private static void AddModules(List<Type> moduleTypes, Type moduleType)
    {
        CoreModuleBase.CheckCoreModuleType(moduleType);
        if (moduleTypes.Contains(moduleType))
            return;
        moduleTypes.Add(moduleType);
        foreach (var dependedModuleType in FindDependedModuleTypes(moduleType))
        {
            AddModules(moduleTypes, dependedModuleType);
        }
    }
}