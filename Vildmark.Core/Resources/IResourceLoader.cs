using System.IO;
using System.Reflection;

namespace Vildmark.Resources
{
    public interface IResourceLoader<T>
    {
        T? Load(Stream stream, Assembly? assembly, string resourceName);
    }
}
