using System.Reflection;

namespace Vildmark.Resources;

public static class ResourceLoader
{
    public static TResource LoadFile<TResource>(string name, string? rootPath = default)
        where TResource : IResource<TResource>
    {
        return Load<TResource>(name, new FileResourceLoadContext(rootPath));
    }

    public static TResource LoadEmbedded<TResource>(string name, Assembly? assembly = default)
        where TResource : IResource<TResource>
    {
        return Load<TResource>(name, new EmbeddedResourceLoadContext(assembly ?? Assembly.GetCallingAssembly()));
    }

    internal static TResource Load<TResource>(string name, ResourceLoadContext context)
        where TResource : IResource<TResource>
    {
        return TResource.Loader.Load(name, context);
    }
}
