using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Resources
{
    public interface IResourceLoaderOptions<TResource, TOptions> : IResourceLoader<TResource>
    {
        TOptions Options { get; set; }
    }
}
