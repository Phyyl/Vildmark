using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Meshes
{
    public class Mesh<TVertex> : IMesh<TVertex>
        where TVertex : unmanaged
    {
        internal GLBuffer<TVertex> VertexBuffer { get; }

        public int ElementSize { get; } = Marshal.SizeOf<TVertex>();

        public virtual int Count => VertexBuffer.Count;

        public Mesh(Span<TVertex> vertices = default)
        {
            VertexBuffer = new GLBuffer<TVertex>(vertices);
        }

        public void UpdateVertices(Span<TVertex> vertices)
        {
            VertexBuffer.SetData(vertices);
        }

        public virtual void Draw(PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            GL.DrawArrays(primitiveType, 0, Count);
        }
    }
}

