using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Vildmark.Resources
{
    [Register(typeof(IResourceLoader<string>))]
    [Register(typeof(IResourceLoader<byte[]>))]
    public class ResourceLoader : IResourceLoader<string>, IResourceLoader<byte[]>
    {
        public static Stream GetEmbeddedStream(string name, Assembly assembly = default)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            assembly ??= Assembly.GetCallingAssembly();

            return assembly.GetManifestResourceStream(name) ?? FindEmbeddedStream(name, assembly);

        }

        public static Stream FindEmbeddedStream(string name, Assembly assembly = default)
        {
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

        public static T LoadEmbedded<T>(string name, Assembly assembly = default) where T : class
        {
            return Service<INamedResourceLoader<T>>.Instance?.Load(name, assembly) ?? Load<T>(GetEmbeddedStream(name, assembly));
        }

        public static T Load<T>(Stream stream) where T : class
        {
            return Service<IResourceLoader<T>>.Instance?.Load(stream);
        }

        byte[] IResourceLoader<byte[]>.Load(Stream stream)
        {
            if (stream is null)
            {
                return null;
            }

            MemoryStream ms = new MemoryStream();

            stream.CopyTo(ms);

            return ms.ToArray();
        }

        string IResourceLoader<string>.Load(Stream stream)
        {
            if (stream is null)
            {
                return null;
            }

            using StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}
