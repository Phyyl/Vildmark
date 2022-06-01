using System.Reflection;

namespace Vildmark.Resources;

public abstract class ResourceLoadContext
{
    public abstract Stream GetStream(string name);

    public TResource Load<TResource>(string name)
        where TResource : IResource<TResource>
    {
        return ResourceLoader.Load<TResource>(name, this);
    }
}

public class EmbeddedResourceLoadContext : ResourceLoadContext
{
    public Assembly Assembly { get; }

    public EmbeddedResourceLoadContext(Assembly assembly)
    {
        Assembly = assembly;
    }

    public override Stream GetStream(string name)
    {
        return EmbeddedResources.GetEmbeddedStream(name, Assembly);
    }
}

public class FileResourceLoadContext : ResourceLoadContext
{
    public string RootPath { get; }

    public FileResourceLoadContext(string? rootPath = default)
    {
        RootPath = rootPath ?? Directory.GetCurrentDirectory();
    }

    public override Stream GetStream(string name)
    {
        return File.OpenRead(Path.Combine(RootPath, name));
    }
}


