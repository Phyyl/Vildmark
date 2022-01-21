using System.Reflection;

namespace Vildmark.Resources
{
    [Register(typeof(IResourceLoader<string>))]
    [Register(typeof(IResourceLoader<string[]>))]
    [Register(typeof(IResourceLoader<byte[]>))]
    public class ResourceLoader : IResourceLoader<string>, IResourceLoader<string[]>, IResourceLoader<byte[]>
    {
        public static Stream? GetEmbeddedStream(string name, Assembly? assembly = default)
        {
            assembly ??= Assembly.GetCallingAssembly();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return assembly.GetManifestResourceStream(name) ?? FindEmbeddedStream(name, assembly);

        }

        public static Stream? FindEmbeddedStream(string name, Assembly? assembly = default)
        {
            assembly ??= Assembly.GetCallingAssembly();

            string[] names = assembly.GetManifestResourceNames();

            if (!names.Contains(name))
            {
                string? closeName = names.FirstOrDefault(n => string.Equals(name, n, StringComparison.InvariantCultureIgnoreCase));

                if (closeName is null)
                {
                    closeName = names.FirstOrDefault(n => n.EndsWith(name, StringComparison.InvariantCultureIgnoreCase));
                }

                if (closeName is null)
                {
                    return null;
                }

                name = closeName;
            }

            if (name is { })
            {
                return assembly.GetManifestResourceStream(name);
            }

            return null;
        }

        public static TResource? Load<TResource>(Stream stream)
        {
            return Load<TResource>(stream, null, null);
        }

        public static TResource? Load<TResource, TLoaderOptions>(Stream stream, TLoaderOptions options)
        {
            return Load<TResource, TLoaderOptions>(stream, options, null, null);
        }

        public static TResource? LoadEmbedded<TResource>(string name, Assembly? assembly = default)
        {
            assembly ??= Assembly.GetCallingAssembly();

            return Load<TResource>(GetEmbeddedStream(name, assembly), assembly, name);
        }

        public static TResource? LoadEmbedded<TResource, TLoaderOptions>(string name, TLoaderOptions options, Assembly? assembly)
        {
            assembly ??= Assembly.GetCallingAssembly();

            if (GetEmbeddedStream(name, assembly) is { } stream)
            {
                return Load<TResource, TLoaderOptions>(stream, options, assembly, name);
            }

            return default;
        }

        private static TResource? Load<TResource>(Stream? stream, Assembly? assembly, string? resourceName)
        {
            if (stream is null)
            {
                return default;
            }

            if (!Service.TryGet<IResourceLoader<TResource>>(out var loader))
            {
                return default;
            }

            return loader.Load(stream, assembly, resourceName);
        }

        private static TResource? Load<TResource, TLoaderOptions>(Stream stream, TLoaderOptions options, Assembly? assembly, string? resourceName)
        {
            if (Service.TryGet<IResourceLoaderOptions<TResource, TLoaderOptions>>(out var loaderOptions))
            {
                return loaderOptions.Load(stream, assembly, resourceName, options);
            }

            return Load<TResource>(stream);
        }

        byte[]? IResourceLoader<byte[]>.Load(Stream stream, Assembly? assembly, string? resourceName)
        {
            if (stream is null)
            {
                return null;
            }

            MemoryStream ms = new();

            stream.CopyTo(ms);

            return ms.ToArray();
        }

        string? IResourceLoader<string>.Load(Stream stream, Assembly? assembly, string? resourceName)
        {
            if (stream is null)
            {
                return null;
            }

            using StreamReader reader = new(stream);

            return reader.ReadToEnd();
        }

        string[]? IResourceLoader<string[]>.Load(Stream stream, Assembly? assembly, string? resourceName)
        {
            if (stream is null)
            {
                return null;
            }

            using StreamReader reader = new(stream);

            List<string> lines = new();

            while (reader.ReadLine() is { } line)
            {
                lines.Add(line);
            }

            return lines.ToArray();
        }
    }
}
