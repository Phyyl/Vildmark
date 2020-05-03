using OpenToolkit.Mathematics;
using System.Collections.Generic;
using Vildmark.Graphics.Models;

namespace Vildmark.Graphics.Helpers
{
	public static class CubeHelper
	{
		public static IEnumerable<Vertex> GetRightVertices() => GetRightVertices(Vector3.Zero, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetRightVertices(Vector4 color) => GetRightVertices(Vector3.Zero, Vector3.One, color);
		public static IEnumerable<Vertex> GetRightVertices(Vector3 offset) => GetRightVertices(offset, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetRightVertices(Vector3 offset, Vector4 color) => GetRightVertices(offset, Vector3.One, color);
		public static IEnumerable<Vertex> GetRightVertices(Vector3 offset, Vector3 size) => GetRightVertices(offset, size, Vector4.One);
		public static IEnumerable<Vertex> GetRightVertices(Vector3 offset, Vector3 size, Vector4 color)
		{
			yield return new Vertex(new Vector3(1, 1, 1) * size + offset, new Vector2(0, 0), color, new Vector3(1, 0, 0));
			yield return new Vertex(new Vector3(1, 0, 1) * size + offset, new Vector2(0, 1), color, new Vector3(1, 0, 0));
			yield return new Vertex(new Vector3(1, 0, 0) * size + offset, new Vector2(1, 1), color, new Vector3(1, 0, 0));
			yield return new Vertex(new Vector3(1, 1, 1) * size + offset, new Vector2(0, 0), color, new Vector3(1, 0, 0));
			yield return new Vertex(new Vector3(1, 0, 0) * size + offset, new Vector2(1, 1), color, new Vector3(1, 0, 0));
			yield return new Vertex(new Vector3(1, 1, 0) * size + offset, new Vector2(1, 0), color, new Vector3(1, 0, 0));
		}

		public static IEnumerable<Vertex> GetLeftVertices() => GetLeftVertices(Vector3.Zero, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetLeftVertices(Vector4 color) => GetLeftVertices(Vector3.Zero, Vector3.One, color);
		public static IEnumerable<Vertex> GetLeftVertices(Vector3 offset) => GetLeftVertices(offset, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetLeftVertices(Vector3 offset, Vector4 color) => GetLeftVertices(offset, Vector3.One, color);
		public static IEnumerable<Vertex> GetLeftVertices(Vector3 offset, Vector3 size) => GetLeftVertices(offset, size, Vector4.One);
		public static IEnumerable<Vertex> GetLeftVertices(Vector3 offset, Vector3 size, Vector4 color)
		{
			yield return new Vertex(new Vector3(0, 1, 0) * size + offset, new Vector2(1, 0), color, new Vector3(-1, 0, 0));
			yield return new Vertex(new Vector3(0, 0, 0) * size + offset, new Vector2(0, 0), color, new Vector3(-1, 0, 0));
			yield return new Vertex(new Vector3(0, 0, 1) * size + offset, new Vector2(0, 1), color, new Vector3(-1, 0, 0));
			yield return new Vertex(new Vector3(0, 1, 0) * size + offset, new Vector2(1, 0), color, new Vector3(-1, 0, 0));
			yield return new Vertex(new Vector3(0, 0, 1) * size + offset, new Vector2(0, 1), color, new Vector3(-1, 0, 0));
			yield return new Vertex(new Vector3(0, 1, 1) * size + offset, new Vector2(1, 1), color, new Vector3(-1, 0, 0));
		}

		public static IEnumerable<Vertex> GetTopVertices() => GetTopVertices(Vector3.Zero, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetTopVertices(Vector4 color) => GetTopVertices(Vector3.Zero, Vector3.One, color);
		public static IEnumerable<Vertex> GetTopVertices(Vector3 offset) => GetTopVertices(offset, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetTopVertices(Vector3 offset, Vector4 color) => GetTopVertices(offset, Vector3.One, color);
		public static IEnumerable<Vertex> GetTopVertices(Vector3 offset, Vector3 size) => GetTopVertices(offset, size, Vector4.One);
		public static IEnumerable<Vertex> GetTopVertices(Vector3 offset, Vector3 size, Vector4 color)
		{
			yield return new Vertex(new Vector3(0, 1, 0) * size + offset, new Vector2(0, 0), color, new Vector3(0, 1, 0));
			yield return new Vertex(new Vector3(0, 1, 1) * size + offset, new Vector2(0, 1), color, new Vector3(0, 1, 0));
			yield return new Vertex(new Vector3(1, 1, 1) * size + offset, new Vector2(1, 1), color, new Vector3(0, 1, 0));
			yield return new Vertex(new Vector3(0, 1, 0) * size + offset, new Vector2(0, 0), color, new Vector3(0, 1, 0));
			yield return new Vertex(new Vector3(1, 1, 1) * size + offset, new Vector2(1, 1), color, new Vector3(0, 1, 0));
			yield return new Vertex(new Vector3(1, 1, 0) * size + offset, new Vector2(1, 0), color, new Vector3(0, 1, 0));
		}

		public static IEnumerable<Vertex> GetBottomVertices() => GetBottomVertices(Vector3.Zero, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetBottomVertices(Vector4 color) => GetBottomVertices(Vector3.Zero, Vector3.One, color);
		public static IEnumerable<Vertex> GetBottomVertices(Vector3 offset) => GetBottomVertices(offset, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetBottomVertices(Vector3 offset, Vector4 color) => GetBottomVertices(offset, Vector3.One, color);
		public static IEnumerable<Vertex> GetBottomVertices(Vector3 offset, Vector3 size) => GetBottomVertices(offset, size, Vector4.One);
		public static IEnumerable<Vertex> GetBottomVertices(Vector3 offset, Vector3 size, Vector4 color)
		{
			yield return new Vertex(new Vector3(0, 0, 1) * size + offset, new Vector2(0, 0), color, new Vector3(0, -1, 0));
			yield return new Vertex(new Vector3(0, 0, 0) * size + offset, new Vector2(0, 1), color, new Vector3(0, -1, 0));
			yield return new Vertex(new Vector3(1, 0, 0) * size + offset, new Vector2(1, 1), color, new Vector3(0, -1, 0));
			yield return new Vertex(new Vector3(0, 0, 1) * size + offset, new Vector2(0, 0), color, new Vector3(0, -1, 0));
			yield return new Vertex(new Vector3(1, 0, 0) * size + offset, new Vector2(1, 1), color, new Vector3(0, -1, 0));
			yield return new Vertex(new Vector3(1, 0, 1) * size + offset, new Vector2(1, 0), color, new Vector3(0, -1, 0));
		}

		public static IEnumerable<Vertex> GetFrontVertices() => GetFrontVertices(Vector3.Zero, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetFrontVertices(Vector4 color) => GetFrontVertices(Vector3.Zero, Vector3.One, color);
		public static IEnumerable<Vertex> GetFrontVertices(Vector3 offset) => GetFrontVertices(offset, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetFrontVertices(Vector3 offset, Vector4 color) => GetFrontVertices(offset, Vector3.One, color);
		public static IEnumerable<Vertex> GetFrontVertices(Vector3 offset, Vector3 size) => GetFrontVertices(offset, size, Vector4.One);
		public static IEnumerable<Vertex> GetFrontVertices(Vector3 offset, Vector3 size, Vector4 color)
		{
			yield return new Vertex(new Vector3(0, 1, 1) * size + offset, new Vector2(0, 0), color, new Vector3(0, 0, 1));
			yield return new Vertex(new Vector3(0, 0, 1) * size + offset, new Vector2(0, 1), color, new Vector3(0, 0, 1));
			yield return new Vertex(new Vector3(1, 0, 1) * size + offset, new Vector2(1, 1), color, new Vector3(0, 0, 1));
			yield return new Vertex(new Vector3(0, 1, 1) * size + offset, new Vector2(0, 0), color, new Vector3(0, 0, 1));
			yield return new Vertex(new Vector3(1, 0, 1) * size + offset, new Vector2(1, 1), color, new Vector3(0, 0, 1));
			yield return new Vertex(new Vector3(1, 1, 1) * size + offset, new Vector2(1, 0), color, new Vector3(0, 0, 1));
		}

		public static IEnumerable<Vertex> GetBackVertices() => GetBackVertices(Vector3.Zero, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetBackVertices(Vector4 color) => GetBackVertices(Vector3.Zero, Vector3.One, color);
		public static IEnumerable<Vertex> GetBackVertices(Vector3 offset) => GetBackVertices(offset, Vector3.One, Vector4.One);
		public static IEnumerable<Vertex> GetBackVertices(Vector3 offset, Vector4 color) => GetBackVertices(offset, Vector3.One, color);
		public static IEnumerable<Vertex> GetBackVertices(Vector3 offset, Vector3 size) => GetBackVertices(offset, size, Vector4.One);
		public static IEnumerable<Vertex> GetBackVertices(Vector3 offset, Vector3 size, Vector4 color)
		{
			yield return new Vertex(new Vector3(1, 1, 0) * size + offset, new Vector2(0, 0), color, new Vector3(0, 0, -1));
			yield return new Vertex(new Vector3(1, 0, 0) * size + offset, new Vector2(0, 1), color, new Vector3(0, 0, -1));
			yield return new Vertex(new Vector3(0, 0, 0) * size + offset, new Vector2(1, 1), color, new Vector3(0, 0, -1));
			yield return new Vertex(new Vector3(1, 1, 0) * size + offset, new Vector2(0, 0), color, new Vector3(0, 0, -1));
			yield return new Vertex(new Vector3(0, 0, 0) * size + offset, new Vector2(1, 1), color, new Vector3(0, 0, -1));
			yield return new Vertex(new Vector3(0, 1, 0) * size + offset, new Vector2(1, 0), color, new Vector3(0, 0, -1));
		}
	}
}
