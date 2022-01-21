using System.Reflection;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
    public abstract class EmbeddedShader : Shader
    {
        protected EmbeddedShader(string resourceName)
            : this($"{resourceName}.vert", $"{resourceName}.frag", $"{resourceName}.geom", Assembly.GetCallingAssembly())
        {
        }

        protected EmbeddedShader(string vertexResourceName, string fragmentResourceName, string? geometryResourceName, Assembly assembly)
            : base(
                  LoadSource(vertexResourceName, assembly) ?? throw new Exception("Vertex shader source not found"),
                  LoadSource(fragmentResourceName, assembly) ?? throw new Exception("Fragment shader source not found"),
                  LoadSource(geometryResourceName, assembly))
        {
        }

        private static string? LoadSource(string? resourceName, Assembly assembly) => resourceName is not null ? ResourceLoader.LoadEmbedded<string>(resourceName, assembly) : default;
    }
}
