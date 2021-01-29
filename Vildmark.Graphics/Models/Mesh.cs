using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Models
{
    public class Mesh
    {
        public GLBuffer<Vertex> VertexBuffer { get; }

        public GLBuffer<uint> IndexBuffer { get; }

        public GLVertexArray VertexArray { get; }

        public Mesh(Span<Vertex> vertices = default, Span<uint> indices = default)
        {
            VertexArray = new GLVertexArray();
            VertexBuffer = new GLBuffer<Vertex>(vertices);
            IndexBuffer = indices.Length > 0 ? new GLBuffer<uint>(indices, BufferTarget.ElementArrayBuffer) : default;
        }

        public void Render(PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            using (VertexArray.Bind())
            {
                IndexBuffer?.Bind();

                if (IndexBuffer != default)
                {
                    GL.DrawElements(primitiveType, IndexBuffer.Count, DrawElementsType.UnsignedInt, 0);
                }
                else
                {
                    GL.DrawArrays(primitiveType, 0, VertexBuffer.Count);
                }
            }
        }

        public void UpdateVertices(Span<Vertex> vertices)
        {
            VertexBuffer?.SetData(vertices);
        }

        public void UpdateIndices(Span<uint> indices)
        {
            IndexBuffer?.SetData(indices);
        }
    }
}

