using OpenTK.Graphics.OpenGL4;
using System;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Meshes
{
    public abstract class IndexedMesh<TVertex> : Mesh<TVertex>
        where TVertex : unmanaged
    {
        internal GLBuffer<uint> IndexBuffer { get; }

        public override int Count => IndexBuffer.Count;

        public IndexedMesh(Span<TVertex> vertices = default)
            : base(vertices)
        {
            IndexBuffer = new GLBuffer<uint>(0, BufferTarget.ElementArrayBuffer);
        }

        public void UpdateIndices(Span<uint> indices)
        {
            IndexBuffer.SetData(indices);
        }

        public override void Draw(PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            GL.DrawElements(primitiveType, Count, DrawElementsType.UnsignedInt, 0);
        }
    }
}

