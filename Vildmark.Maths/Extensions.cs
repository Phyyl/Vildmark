using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
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

		public static Vector2 Floor(this Vector2 v)
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

		public static Vector2 Ceiling(this Vector2 v)
		{
			return new Vector2(MathsHelper.Ceiling(v.X), MathsHelper.Ceiling(v.Y));
		}

		public static Vector3 Ceiling(this Vector3 v)
		{
			return new Vector3(MathsHelper.Ceiling(v.X), MathsHelper.Ceiling(v.Y), MathsHelper.Ceiling(v.Z));
		}

		public static Vector4 Ceiling(this Vector4 v)
		{
			return new Vector4(MathsHelper.Ceiling(v.X), MathsHelper.Ceiling(v.Y), MathsHelper.Ceiling(v.Z), MathsHelper.Ceiling(v.W));
		}

		public static Vector2 Abs(this Vector2 v)
        {
			return new Vector2(Math.Abs(v.X), Math.Abs(v.Y));
		}

		public static Vector3 Abs(this Vector3 v)
		{
			return new Vector3(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z));
		}

		public static Vector4 Abs(this Vector4 v)
		{
			return new Vector4(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z), Math.Abs(v.W));
		}

		public static Vector4 ToVector(this Rectangle rectangle)
        {
			return new Vector4(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}

		public static Vector4 ToVector(this RectangleF rectangle)
		{
			return new Vector4(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
		}

		public static Vector2i ToVector2i(this Vector2 v)
		{
			return new Vector2i((int)v.X, (int)v.Y);
		}

		public static Vector3i ToVector3i(this Vector3 v)
        {
			return new Vector3i((int)v.X, (int)v.Y, (int)v.Z);
        }

		public static Vector4i ToVector4i(this Vector4 v)
		{
			return new Vector4i((int)v.X, (int)v.Y, (int)v.Z, (int)v.W);
		}

		public static RectangleF ToRectangleF(this Vector4 v)
        {
			return new RectangleF(v.X, v.Y, v.Z, v.W);
		}
	}
}
