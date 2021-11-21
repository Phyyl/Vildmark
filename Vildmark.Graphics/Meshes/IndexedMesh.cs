using OpenTK.Graphics.OpenGL4;
using System;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Meshes
{
    public class IndexedMesh<TVertex> : Mesh, IIndexedMesh where TVertex : unmanaged
    {
        public GLBuffer<uint> IndexBuffer { get; } = new GLBuffer<uint>(0, BufferTarget.ElementArrayBuffer);

        public IndexedMesh(Span<Vertex> vertices = default)
            : base(vertices)
        {
        }

        public override void Render(PrimitiveType primitiveType)
        {
            GL.DrawElements(primitiveType, IndexBuffer.Count, DrawElementsType.UnsignedInt, 0);
        }
    }

    public class IndexedMesh : IndexedMesh<Vertex>
    {
        public IndexedMesh(Span<Vertex> vertices = default)
            : base(vertices)
        {
        }
    }
}

