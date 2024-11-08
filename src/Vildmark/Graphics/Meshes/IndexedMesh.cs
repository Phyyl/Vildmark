using OpenTK.Graphics.OpenGL;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Meshes;

public abstract class IndexedMesh<TVertex>(Span<TVertex> vertices = default, Span<uint> indices = default) : Mesh<TVertex>(vertices)
    where TVertex : unmanaged
{
    internal GLBuffer<uint> IndexBuffer { get; } = new GLBuffer<uint>(indices, BufferTarget.ElementArrayBuffer);

    public override int Count => IndexBuffer.Count;

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

public class IndexedMesh(Span<Vertex> vertices = default, Span<uint> indices = default) : IndexedMesh<Vertex>(vertices, indices)
{
}

