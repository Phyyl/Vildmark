namespace Vildmark.Resources;

public interface IResourceLoader<T>
    where T : IResource<T>
{
    T Load(string name, ResourceLoadContext context);
}

public interface IResource<T>
    where T : IResource<T>
{
    static abstract IResourceLoader<T> Loader { get; }
}
