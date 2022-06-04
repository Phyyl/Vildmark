using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Vildmark.Resources;

public static class EmbeddedResources
{
    public static Stream GetEmbeddedStream(string resourceName, Assembly assembly)
    {
        return TryGetEmbeddedStream(resourceName, assembly, out var stream) ? stream : throw new EmbeddedResourceNotFoundException(resourceName, assembly);
    }

    public static bool TryGetEmbeddedStream(string resourceName, Assembly assembly, [NotNullWhen(true)] out Stream? stream)
    {
        stream = null;

        if (FindResourceName(resourceName, assembly) is string foundResourceName)
        {
            stream = assembly.GetManifestResourceStream(foundResourceName);
        }

        return stream is not null;
    }

    public static string? FindResourceName(string resourceName, Assembly assembly)
    {
        string[] names = assembly.GetManifestResourceNames();

        return names.Contains(resourceName)
            ? resourceName
            : (names.FirstOrDefault(n => string.Equals(resourceName, n, StringComparison.InvariantCultureIgnoreCase)) ??
                names.FirstOrDefault(n => n.EndsWith(resourceName, StringComparison.InvariantCultureIgnoreCase)));
    }
}
