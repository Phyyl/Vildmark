namespace Vildmark.Resources;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ResourceLoaderAttribute(Type type) : Attribute
{
    public Type Type { get; } = type;
}
