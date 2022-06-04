using System.Reflection;
using Vildmark.Helpers;

namespace Vildmark.Resources;

public static class ResourceLoader
{
    public static TResource LoadFile<TResource>(string name, string? rootPath = default)
    {
        return Load<TResource>(name, new FileResourceLoadContext(rootPath));
    }

    public static TResource LoadEmbedded<TResource>(string name, Assembly? assembly = default)
    {
        return Load<TResource>(name, new EmbeddedResourceLoadContext(assembly ?? Assembly.GetCallingAssembly()));
    }

    internal static TResource Load<TResource>(string name, ResourceLoadContext context)
    {
        return GetResourceLoader<TResource>().Load(name, context);
    }

    internal static TResource Load<TResource, TOptions>(string name, TOptions options, ResourceLoadContext context)
    {
        if (GetResourceLoader<TResource>() is not IResourceLoader<TResource,TOptions> loader)
        {
            throw new Exception($"Loader for type {typeof(TResource).Name} does not support options of type {typeof(TOptions).Name}");
        }

        return loader.Load(name, options, context);
    }

    private static IResourceLoader<TResource> GetResourceLoader<TResource>()
    {
        return LoaderStorage<TResource>.Loader ??= ResolveResourceLoader<TResource>() ?? throw new Exception($"Failed to resolve resource loader for type: {typeof(TResource).Name}");
    }

    private static IResourceLoader<TResource>? ResolveResourceLoader<TResource>()
    {
        if (typeof(TResource).GetCustomAttribute<ResourceLoaderAttribute>() is not { } attribute)
        {
            return default;
        }

        if (typeof(IResourceLoader<TResource>).IsAssignableFrom(attribute.Type) && TypeHelper.TryCreateIsntance(attribute.Type, out var result) && result is IResourceLoader<TResource> loader)
        {
            return loader;
        }

        return default;
    }

    private static class LoaderStorage<T>
    {
        public static IResourceLoader<T>? Loader { get; set; }
    }
}

public interface IResourceLoader<TResource>
{
    TResource Load(string name, ResourceLoadContext context);
}

public interface IResourceLoader<TResource, TOptions> : IResourceLoader<TResource>
{
    TResource Load(string name, TOptions options, ResourceLoadContext context);
}
