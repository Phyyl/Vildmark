using OpenTK.Graphics.OpenGL;
using Vildmark.Logging;

namespace Vildmark.Graphics.GLObjects;

internal class GLShaderProgram : GLObject
{
    public GLShaderProgram()
        : base(GL.CreateProgram())
    {
    }

    public string InfoLog
    {
        get
        {
            GL.GetProgramInfoLog(this, out var result);
            return result;
        }
    }

    public bool IsLinked
    {
        get
        {
            GL.GetProgrami(this, ProgramProperty.LinkStatus, out int value);

            return value == 1;
        }
    }

    public void AttachShader(GLShader shader)
    {
        GL.AttachShader(this, shader);
    }

    public bool Link()
    {
        GL.LinkProgram(this);

        if (!IsLinked)
        {
            Logger.Error($"Shader program info log: {InfoLog}");
        }
        else if (InfoLog is { Length: > 0 })
        {
            Logger.Warning($"Shader program info log: {InfoLog}");
        }

        return IsLinked;
    }

    public void Use()
    {
        GL.UseProgram(this);
    }

    public int GetUniformLocation(string name)
    {
        return GL.GetUniformLocation(this, name);
    }

    public int GetAttribLocation(string name)
    {
        return GL.GetAttribLocation(this, name);
    }

    public bool Uniform<T>(string name, T value) => Uniform(GetUniformLocation(name), value);
    public bool Uniform<T>(int location, T value) => StaticTypeInfo.SetUniform(location, value);

    protected override void DisposeOpenGL(ref int id)
    {
        GL.DeleteProgram(id);
    }

    public static void Unuse()
    {
        GL.UseProgram(0);
    }
}
