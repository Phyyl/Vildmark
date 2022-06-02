using OpenTK.Graphics.OpenGL4;
using Vildmark.Graphics.GLObjects.Loaders;
using Vildmark.Logging;
using Vildmark.Resources;

namespace Vildmark.Graphics.GLObjects;

public class GLShader : GLObject, IResource<GLShader>
{
    public static IResourceLoader<GLShader> Loader { get; } = new GLShaderResourceLoader();

    public string InfoLog => GL.GetShaderInfoLog(this);

    public bool Compiled
    {
        get
        {
            GL.GetShader(this, ShaderParameter.CompileStatus, out int value);

            return value == 1;
        }
    }

    public GLShader(ShaderType shaderType, string source)
        : base(GL.CreateShader(shaderType))
    {
        GL.ShaderSource(this, source);
        GL.CompileShader(this);

        if (!Compiled)
        {
            Logger.Error($"{shaderType} info log: {InfoLog}");
        }
        else if (InfoLog is { Length: > 0 })
        {
            Logger.Warning($"{shaderType} info log: {InfoLog}");
        }
    }

    protected override void DisposeOpenGL()
    {
        GL.DeleteShader(this);
    }
}
