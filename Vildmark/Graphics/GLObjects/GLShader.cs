using OpenTK.Graphics.OpenGL4;
using Vildmark.Graphics.Shaders;
using Vildmark.Logging;

namespace Vildmark.Graphics.GLObjects;

internal class GLShader : GLObject
{
    public string InfoLog => GL.GetShaderInfoLog(this);

    public bool IsCompiled
    {
        get
        {
            GL.GetShader(this, ShaderParameter.CompileStatus, out int value);

            return value == 1;
        }
    }

    public ShaderType ShaderType { get; }

    public GLShader(ShaderType shaderType, string source)
        : base(GL.CreateShader(shaderType))
    {
        GL.ShaderSource(this, source);
        ShaderType = shaderType;
    }

    public bool Compile()
    {
        GL.CompileShader(this);

        if (!IsCompiled)
        {
            Logger.Error($"{ShaderType} info log: {InfoLog}");
        }
        else if (InfoLog is { Length: > 0 })
        {
            Logger.Warning($"{ShaderType} info log: {InfoLog}");
        }

        return IsCompiled;
    }

    protected override void DisposeOpenGL()
    {
        GL.DeleteShader(this);
    }
}
