using System.IO;

namespace Vildmark.Resources
{
    public interface IResourceLoader<T>
    {
        T Load(Stream stream);
    }
}
