using System.Reflection;

namespace Vildmark.Resources;

public class EmbeddedResourceNotFoundException : Exception
{
    public EmbeddedResourceNotFoundException(string resourceName, Assembly assembly)
        : base($@"Resource not found: ""{resourceName}"" in {assembly}")
    {
    }
}
