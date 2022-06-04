namespace Vildmark.Resources;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ResourceLoaderAttribute : Attribute
{
    public Type Type { get; }

    public ResourceLoaderAttribute(Type type)
    {
        Type = type;
    }
}
