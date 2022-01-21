using System.Reflection;

namespace Vildmark.Resources
{
    public interface IResourceLoaderOptions<TResource, TOptions>
    {
        TResource Load(Stream stream, Assembly? assembly, string? resourceName, TOptions options);
    }
}
