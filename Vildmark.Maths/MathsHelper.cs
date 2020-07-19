using OpenToolkit.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vildmark.Maths
{
	public static class MathsHelper
	{
		public static float Round(float value, int digits = 0) => (float)Math.Round(value, digits);
		public static float Floor(float value) => (float)Math.Floor(value);

		public static float Min(float a, float b) => a < b ? a : b;
		public static float Max(float a, float b) => a > b ? a : b;

		public static Vector2 Min(Vector2 a, Vector2 b) => new Vector2(Min(a.X, b.X), Min(a.Y, b.Y));
		public static Vector3 Min(Vector3 a, Vector3 b) => new Vector3(Min(a.X, b.X), Min(a.Y, b.Y), Min(a.Z, b.Z));
		public static Vector4 Min(Vector4 a, Vector4 b) => new Vector4(Min(a.X, b.X), Min(a.Y, b.Y), Min(a.Z, b.Z), Min(a.W, b.W));

		public static Vector2 Max(Vector2 a, Vector2 b) => new Vector2(Max(a.X, b.X), Max(a.Y, b.Y));
		public static Vector3 Max(Vector3 a, Vector3 b) => new Vector3(Max(a.X, b.X), Max(a.Y, b.Y), Max(a.Z, b.Z));
		public static Vector4 Max(Vector4 a, Vector4 b) => new Vector4(Max(a.X, b.X), Max(a.Y, b.Y), Max(a.Z, b.Z), Max(a.W, b.W));

		public static void ConvertTo3DIndex(int index, int width, int height, int depth, out int x, out int y, out int z)
		{
			x = index / (height * depth);
			y = (index / depth) % height;
			z = index % depth;
		}
	}
}
