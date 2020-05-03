using System;

namespace Vildmark.Rendering
{
	internal class Mesh
	{
		public Mesh(Span<Vertex3D> vertices, Span<uint> indices = default)
		{
			Vertices = vertices.ToArray();
			Indices = indices.ToArray();
		}

		public Memory<Vertex3D> Vertices { get; }

		public Memory<uint> Indices { get; }
	}
}