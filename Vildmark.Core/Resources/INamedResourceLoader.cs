using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Vildmark.Resources
{
    public interface INamedResourceLoader<T> where T : class
    {
        T Load(string name, Assembly assembly = default);
    }
}
