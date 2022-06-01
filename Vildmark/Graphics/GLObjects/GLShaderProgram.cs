using OpenTK.Graphics.OpenGL4;
using Vildmark.Graphics.GLObjects.Loaders;
using Vildmark.Resources;

namespace Vildmark.Graphics.GLObjects;

public class GLShaderProgram : GLObject, IResource<GLShaderProgram>
{
    public static IResourceLoader<GLShaderProgram> Loader { get; } = new GLShaderProgramResourceLoader();

    public GLShaderProgram(params GLShader[] shaders)
        : base(GL.CreateProgram())
    {
        foreach (var shader in shaders.NotNull())
        {
            GL.AttachShader(this, shader);
        }

        GL.LinkProgram(this);

        if (!Linked)
        {
            Logger.Error($"Shader program info log: {InfoLog}");
        }
        else if (InfoLog is { Length: > 0 })
        {
            Logger.Warning($"Shader program info log: {InfoLog}");
        }
    }

    public string InfoLog => GL.GetProgramInfoLog(this);

    public bool Linked
    {
        get
        {
            GL.GetProgram(this, GetProgramParameterName.LinkStatus, out int value);

            return value == 1;
        }
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

    protected override void DisposeOpenGL()
    {
        GL.DeleteProgram(this);
    }

    public static void Unuse()
    {
        GL.UseProgram(0);
    }
}
