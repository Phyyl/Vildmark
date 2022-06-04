using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics.Shaders;

public abstract record ShaderVariable(string Name)
{
    public int Location { get; internal protected set; }

    public bool IsDefined => Location >= 0;
}

public abstract record Attrib(string Name) : ShaderVariable(Name)
{
    public abstract void VertexAttribPointer();
}

public record Attrib<TAttrib>(string Name, int Offset = 0, int Stride = 0) : Attrib(Name)
{
    public VertexAttribPointerType VertexAttribPointerType { get; } = StaticTypeInfo.GetAttribType<TAttrib>();
    public int Size { get; } = StaticTypeInfo.GetAttribSize<TAttrib>();

    public override void VertexAttribPointer()
    {
        if (!IsDefined)
        {
            return;
        }

        GL.VertexAttribPointer(Location, Size, VertexAttribPointerType, false, Stride, Offset);
        GL.EnableVertexAttribArray(Location);
    }
}

public record Attrib<TVertex, TAttrib> : Attrib<TAttrib>
    where TVertex : unmanaged
{
    public Attrib(string name, string fieldName)
        : this(name, (int)Marshal.OffsetOf<TVertex>(fieldName))
    {

    }

    public unsafe Attrib(string name, int offset)
        : base(name, offset, sizeof(TVertex))
    {
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
