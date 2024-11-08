using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Meshes;

public class Mesh<TVertex>
    where TVertex : unmanaged
{
    internal GLVertexArray VertexArray { get; }
    internal GLBuffer<TVertex> VertexBuffer { get; }

    public int ElementSize { get; } = Marshal.SizeOf<TVertex>();

    public virtual int Count => VertexBuffer.Count;

    public Mesh(Span<TVertex> vertices = default)
    {
        VertexArray = new();
        VertexBuffer = new GLBuffer<TVertex>(vertices);

        VertexArray.Bind();
        VertexBuffer.Bind();
    }

    public void UpdateVertices(Span<TVertex> vertices)
    {
        VertexBuffer.SetData(vertices);
    }

    public virtual void Draw(PrimitiveType primitiveType = PrimitiveType.Triangles)
    {
        VertexArray.Bind();
        GL.DrawArrays(primitiveType, 0, Count);
    }
}

public class Mesh(Span<Vertex> vertices = default) : Mesh<Vertex>(vertices)
{
}

