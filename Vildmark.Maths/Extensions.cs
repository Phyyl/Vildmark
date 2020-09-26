using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Vildmark.Maths
{
	public static class Extensions
	{
		public static Vector2 Round(this Vector2 v, int digits = 0)
		{
			return new Vector2(MathsHelper.Round(v.X, digits), MathsHelper.Round(v.Y, digits));
		}

		public static Vector3 Round(this Vector3 v, int digits = 0)
		{
			return new Vector3(MathsHelper.Round(v.X, digits), MathsHelper.Round(v.Y, digits), MathsHelper.Round(v.Z, digits));
		}

		public static Vector4 Round(this Vector4 v, int digits = 0)
		{
			return new Vector4(MathsHelper.Round(v.X, digits), MathsHelper.Round(v.Y, digits), MathsHelper.Round(v.Z, digits), MathsHelper.Round(v.W, digits));
		}

		public static Vector2 Floored(this Vector2 v)
		{
			return new Vector2(MathsHelper.Floor(v.X), MathsHelper.Floor(v.Y));
		}

		public static Vector3 Floor(this Vector3 v)
		{
			return new Vector3(MathsHelper.Floor(v.X), MathsHelper.Floor(v.Y), MathsHelper.Floor(v.Z));
		}

		public static Vector4 Floor(this Vector4 v)
		{
			return new Vector4(MathsHelper.Floor(v.X), MathsHelper.Floor(v.Y), MathsHelper.Floor(v.Z), MathsHelper.Floor(v.W));
		}

		public static Vector4 ToVector(this Rectangle rectangle)
        {
			return new Vector4(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}

		public static Vector4 ToVector(this RectangleF rectangle)
		{
			return new Vector4(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}

		public static RectangleF ToRectangleF(this Vector4 v)
        {
			return new RectangleF(v.X, v.Y, v.Z, v.W);
		}
	}
}
