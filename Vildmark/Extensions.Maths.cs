using OpenTK.Mathematics;
using System.Drawing;

namespace Vildmark;

public static partial class Extensions
{
    public static Vector2 Round(this Vector2 v, int digits = 0) => new(MathF.Round(v.X, digits), MathF.Round(v.Y, digits));
    public static Vector3 Round(this Vector3 v, int digits = 0) => new(MathF.Round(v.X, digits), MathF.Round(v.Y, digits), MathF.Round(v.Z, digits));
    public static Vector4 Round(this Vector4 v, int digits = 0) => new(MathF.Round(v.X, digits), MathF.Round(v.Y, digits), MathF.Round(v.Z, digits), MathF.Round(v.W, digits));

    public static Vector2 Floor(this Vector2 v) => new(MathF.Floor(v.X), MathF.Floor(v.Y));
    public static Vector3 Floor(this Vector3 v) => new(MathF.Floor(v.X), MathF.Floor(v.Y), MathF.Floor(v.Z));
    public static Vector4 Floor(this Vector4 v) => new(MathF.Floor(v.X), MathF.Floor(v.Y), MathF.Floor(v.Z), MathF.Floor(v.W));

    public static Vector2 Ceiling(this Vector2 v) => new(MathF.Ceiling(v.X), MathF.Ceiling(v.Y));
    public static Vector3 Ceiling(this Vector3 v) => new(MathF.Ceiling(v.X), MathF.Ceiling(v.Y), MathF.Ceiling(v.Z));
    public static Vector4 Ceiling(this Vector4 v) => new(MathF.Ceiling(v.X), MathF.Ceiling(v.Y), MathF.Ceiling(v.Z), MathF.Ceiling(v.W));

    public static Vector2 Abs(this Vector2 v) => new(Math.Abs(v.X), Math.Abs(v.Y));
    public static Vector3 Abs(this Vector3 v) => new(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z));
    public static Vector4 Abs(this Vector4 v) => new(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z), Math.Abs(v.W));

    public static Vector4 ToVector(this Rectangle rectangle) => new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
    public static Vector4 ToVector(this RectangleF rectangle) => new(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

    public static Vector2i ToVector2i(this Vector2 v) => new((int)v.X, (int)v.Y);
    public static Vector3i ToVector3i(this Vector3 v) => new((int)v.X, (int)v.Y, (int)v.Z);
    public static Vector4i ToVector4i(this Vector4 v) => new((int)v.X, (int)v.Y, (int)v.Z, (int)v.W);

    public static RectangleF ToRectangleF(this Vector4 v) => new(v.X, v.Y, v.Z, v.W);
    public static RectangleF Inflated(this RectangleF rectangle, Vector2 size) => rectangle.Inflated(size.X, size.Y);
    public static RectangleF Inflated(this RectangleF rectangle, float x, float y) => new(rectangle.X - x, rectangle.Y - y, rectangle.Width + x + x, rectangle.Height + y + y);
    public static RectangleF Translated(this RectangleF rectangle, Vector2 offset) => rectangle.Translated(offset.X, offset.Y);
    public static RectangleF Translated(this RectangleF rectangle, float x, float y) => new(rectangle.X + x, rectangle.Y + y, rectangle.Width, rectangle.Height);

    public static Vector2 GetCenter(this RectangleF rectangle) => new((rectangle.Left + rectangle.Right) / 2f, (rectangle.Top + rectangle.Bottom) / 2f);

    public static Vector2 GetTopLeft(this RectangleF rectangle) => new(rectangle.Left, rectangle.Top);
    public static Vector2 GetBottomLeft(this RectangleF rectangle) => new(rectangle.Left, rectangle.Bottom);
    public static Vector2 GetBottomRight(this RectangleF rectangle) => new(rectangle.Right, rectangle.Bottom);
    public static Vector2 GetTopRight(this RectangleF rectangle) => new(rectangle.Right, rectangle.Top);

    public static Vector2 ToVector(this SizeF size) => new(size.Width, size.Height);
    public static Vector2 ToVector(this PointF size) => new(size.X, size.Y);
    public static Vector4 ToVector(this Color4 color) => new(color.R, color.G, color.B, color.A);

    public static Color4 ToColor4(this Vector4 color) => new(color.X, color.Y, color.Z, color.W);
}
