using System;

namespace Vildmark.Graphics.Models
{
	public class MeshDescriptor
	{
		public MeshDescriptor(Span<Vertex> vertices, Span<uint> indices = default)
		{
			Vertices = vertices.ToArray();
			Indices = indices.ToArray();
		}

		public Memory<Vertex> Vertices { get; }

		public Memory<uint> Indices { get; }
	}
}