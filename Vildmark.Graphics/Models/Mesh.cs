using OpenToolkit.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Models
{
	public class Mesh
	{
		public GLBuffer<Vertex> VertexBuffer { get; }

		public GLBuffer<uint> IndexBuffer { get; }

		public GLVertexArray VertexArray { get; }

		public Mesh(MeshDescriptor meshDescriptor)
		{
			VertexArray = new GLVertexArray();
			VertexBuffer = new GLBuffer<Vertex>(meshDescriptor.Vertices.Span);
			IndexBuffer = meshDescriptor.Indices.Length > 0 ? new GLBuffer<uint>(meshDescriptor.Indices.Span, BufferTarget.ElementArrayBuffer) : default;

			using (VertexArray.Bind())
			{
				IndexBuffer?.Bind();

				VertexBuffer.VertexAttribPointer(0, 3, stride: Vertex.SizeInBytes, offset: Vertex.PositionOffset);
				VertexBuffer.VertexAttribPointer(1, 2, stride: Vertex.SizeInBytes, offset: Vertex.TexCoordOffset);
				VertexBuffer.VertexAttribPointer(2, 4, stride: Vertex.SizeInBytes, offset: Vertex.ColorOffset);
				VertexBuffer.VertexAttribPointer(3, 3, stride: Vertex.SizeInBytes, offset: Vertex.NormalOffset);
			}
		}

		public void Render(PrimitiveType primitiveType = PrimitiveType.Triangles)
		{
			using (VertexArray.Bind())
			{
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
	}
}

