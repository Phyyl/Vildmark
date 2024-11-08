using OpenTK.Graphics.OpenGL;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Meshes;

public abstract class IndexedMesh<TVertex> : Mesh<TVertex>
    where TVertex : unmanaged
{
    internal GLBuffer<uint> IndexBuffer { get; }

    public override int Count => IndexBuffer.Count;

    public IndexedMesh(Span<TVertex> vertices = default, Span<uint> indices = default)
        : base(vertices)
    {
        IndexBuffer = new GLBuffer<uint>(indices, BufferTarget.ElementArrayBuffer);
    }

    public void UpdateIndices(Span<uint> indices)
    {
        IndexBuffer.SetData(indices);
    }

    public override void Draw(PrimitiveType primitiveType = PrimitiveType.Triangles)
    {
        VertexArray.Bind();

        GL.DrawElements(primitiveType, Count, DrawElementsType.UnsignedInt, 0);
    }
}

public class IndexedMesh : IndexedMesh<Vertex>
{
    public IndexedMesh(Span<Vertex> vertices = default, Span<uint> indices = default)
        : base(vertices, indices)
    {
    }
}

