using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Vildmark.Resources
{
    [Register(typeof(IResourceLoader<string>))]
    [Register(typeof(IResourceLoader<byte[]>))]
    public class ResourceLoader : IResourceLoader<string>, IResourceLoader<byte[]>
    {
        public static Stream GetEmbeddedStream(string name, Assembly assembly = default)
        {
            assembly ??= Assembly.GetCallingAssembly();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return assembly.GetManifestResourceStream(name) ?? FindEmbeddedStream(name, assembly);

        }

        public static Stream FindEmbeddedStream(string name, Assembly assembly = default)
        {
            assembly ??= Assembly.GetCallingAssembly();

            string[] names = assembly.GetManifestResourceNames();

            if (!names.Contains(name))
            {
                string closeName = names.FirstOrDefault(n => string.Equals(name, n, StringComparison.InvariantCultureIgnoreCase));

                if (closeName is null)
                {
                    closeName = names.FirstOrDefault(n => n.EndsWith(name, StringComparison.InvariantCultureIgnoreCase));
                }

                name = closeName;
            }

            if (name is { })
            {
                return assembly.GetManifestResourceStream(name);
            }

            return null;
        }

        public static TResource Load<TResource>(Stream stream)
        {
            if (!Service.TryGet<IResourceLoader<TResource>>(out var loader))
            {
                return default;
            }

            return loader.Load(stream);
        }

        public static TResource Load<TResource, TLoaderOptions>(Stream stream, TLoaderOptions options)
        {
            if (Service.TryGet<IResourceLoaderOptions<TResource, TLoaderOptions>>(out var loaderOptions))
            {
                loaderOptions.Options = options;
            }

            return Load<TResource>(stream);
        }

        public static TResource LoadEmbedded<TResource>(string name) => LoadEmbedded<TResource>(name, Assembly.GetCallingAssembly());
        public static TResource LoadEmbedded<TResource>(string name, Assembly assembly)
        {
            return Load<TResource>(GetEmbeddedStream(name, assembly));
        }

        public static TResource LoadEmbedded<TResource, TLoaderOptions>(string name, TLoaderOptions options) => LoadEmbedded<TResource, TLoaderOptions>(name, options, Assembly.GetCallingAssembly());
        public static TResource LoadEmbedded<TResource, TLoaderOptions>(string name, TLoaderOptions options, Assembly assembly)
        {
            return Load<TResource, TLoaderOptions>(GetEmbeddedStream(name, assembly), options);
        }

        byte[] IResourceLoader<byte[]>.Load(Stream stream)
        {
            if (stream is null)
            {
                return null;
            }

            MemoryStream ms = new();

            stream.CopyTo(ms);

            return ms.ToArray();
        }

        string IResourceLoader<string>.Load(Stream stream)
        {
            if (stream is null)
            {
                return null;
            }

            using StreamReader reader = new(stream);

            return reader.ReadToEnd();
        }
    }
}
