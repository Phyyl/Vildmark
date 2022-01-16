using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Resources
{
    public interface IResourceLoaderOptions<TResource, TOptions>
    {
        TResource Load(Stream stream, Assembly? assembly, string resourceName, TOptions options);
    }
}
