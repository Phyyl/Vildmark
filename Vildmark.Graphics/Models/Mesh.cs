using OpenToolkit.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Text;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Models
{
	public class Mesh
	{
		public GLBuffer<Vertex> VertexBuffer { get; private set; }

		public GLBuffer<uint> IndexBuffer { get; private set; }

		public GLVertexArray VertexArray { get; }

		public Mesh(Span<Vertex> vertices = default, Span<uint> indices = default)
		{
			VertexArray = new GLVertexArray();
			VertexBuffer = new GLBuffer<Vertex>(vertices);
			IndexBuffer = indices.Length > 0 ? new GLBuffer<uint>(indices, BufferTarget.ElementArrayBuffer) : default;

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

		public void UpdateVertices(Span<Vertex> vertices)
		{
			VertexBuffer?.UpdateData(vertices);
		}

		public void UpdateIndices(Span<uint> indices)
		{
			IndexBuffer?.UpdateData(indices);
		}
	}
}

