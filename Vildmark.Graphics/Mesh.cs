using OpenTK.Graphics.OpenGL;
using System;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics
{
    public abstract class Mesh
    {
        public Transform Transform { get; } = new();

        public GLVertexArray VertexArray { get; } = new GLVertexArray();

        public void Render(PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            VertexArray.Bind();
            OnRender(primitiveType);
        }

        protected abstract void OnRender(PrimitiveType primitiveType);
    }

    public class Mesh<TVertex> : Mesh where TVertex : unmanaged
    {
        public GLBuffer<TVertex> VertexBuffer { get; }

        public Mesh(Span<TVertex> vertices = default)
        {
            VertexBuffer = new GLBuffer<TVertex>(vertices);
        }

        protected override void OnRender(PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            GL.DrawArrays(primitiveType, 0, VertexBuffer.Count);
        }
    }

    public class IndexedMesh<TVertex> : Mesh<TVertex> where TVertex : unmanaged
    {
        public GLBuffer<uint> IndexBuffer { get; } = new GLBuffer<uint>(0, BufferTarget.ElementArrayBuffer);

        protected override void OnRender(PrimitiveType primitiveType)
        {
            GL.DrawElements(primitiveType, IndexBuffer.Count, DrawElementsType.UnsignedInt, 0);
        }
    }
}

