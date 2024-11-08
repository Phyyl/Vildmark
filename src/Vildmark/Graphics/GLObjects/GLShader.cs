using OpenTK.Graphics.OpenGL;
using Vildmark.Logging;

namespace Vildmark.Graphics.GLObjects;

internal class GLShader : GLObject
{
    public string InfoLog
    {
        get
        {
            GL.GetShaderInfoLog(this, out var result);
            return result;
        }
    }

    public bool IsCompiled
    {
        get
        {
            GL.GetShaderi(this, ShaderParameterName.CompileStatus, out int value);

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

    protected override void DisposeOpenGL(ref int id)
    {
        GL.DeleteShader(id);
    }
}
