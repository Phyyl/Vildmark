using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics.Shaders;

public abstract record ShaderVariable(string Name)
{
    public int Location { get; internal protected set; }

    public bool IsDefined => Location >= 0;
}

public abstract record Attrib(string Name) : ShaderVariable(Name);

public record Attrib<T>(string Name) : Attrib(Name)
{
    public VertexAttribPointerType VertexAttribPointerType { get; } = StaticTypeInfo.GetAttribType<T>();
    public int Size { get; } = StaticTypeInfo.GetAttribSize<T>();

    public unsafe void VertexAttribPointer<TVertex>(int offset)
        where TVertex : unmanaged
    {
        if (!IsDefined)
        {
            return;
        }

        GL.VertexAttribPointer(Location, Size, VertexAttribPointerType, false, sizeof(TVertex), offset);
        GL.EnableVertexAttribArray(Location);
    }

    public void VertexAttribPointer<TVertex>(string fieldName)
        where TVertex : unmanaged
    {
        VertexAttribPointer<TVertex>((int)Marshal.OffsetOf<TVertex>(fieldName));
    }
}

public abstract record Uniform(string Name) : ShaderVariable(Name);
public record Uniform<T>(string Name) : Uniform(Name)
{
    public void SetUniform(T value, int index = 0)
    {
        if (!IsDefined)
        {
            return;
        }

        StaticTypeInfo.SetUniform<T>(Location, value, index);
    }
}
