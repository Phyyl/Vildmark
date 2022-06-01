using OpenTK.Graphics.OpenGL4;
using Vildmark.Resources;

namespace Vildmark.Graphics.GLObjects.Loaders;

internal class GLShaderResourceLoader : IResourceLoader<GLShader>
{
    public GLShader Load(string name, ResourceLoadContext context)
    {
        ShaderType shaderType = Path.GetExtension(name) switch
        {
            ".frag" => ShaderType.FragmentShader,
            ".geom" => ShaderType.GeometryShader,
            _ => ShaderType.VertexShader
        };

        return new GLShader(shaderType, context.GetStream(name).ReadAllText());
    }
}
