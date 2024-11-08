using System.Reflection;

namespace Vildmark.Resources;

public class EmbeddedResourceNotFoundException(string resourceName, Assembly assembly) : Exception($@"Resource not found: ""{resourceName}"" in {assembly}")
{
}
