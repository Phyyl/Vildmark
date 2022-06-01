using Vildmark.Resources;

namespace Vildmark.Graphics.GLObjects.Loaders
{
    internal class GLShaderProgramResourceLoader : IResourceLoader<GLShaderProgram>
    {
        public GLShaderProgram Load(string name, ResourceLoadContext context)
        {
            return new GLShaderProgram(context.Load<GLShader>($"{name}.vert"), context.Load<GLShader>($"{name}.frag"));
        }
    }
}
