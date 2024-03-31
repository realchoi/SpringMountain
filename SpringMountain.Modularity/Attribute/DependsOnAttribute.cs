namespace SpringMountain.Modularity.Attribute;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependsOnAttribute : System.Attribute
{
    public Type[] DependedTypes { get; }

    public DependsOnAttribute(params Type[] dependedTypes)
    {
        DependedTypes = dependedTypes ?? Array.Empty<Type>();
    }

    public virtual Type[] GetDependedTypes()
    {
        return DependedTypes;
    }
}