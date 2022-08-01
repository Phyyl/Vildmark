using OpenTK.Mathematics;
using Vildmark.Maths.Physics;

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

    public static Vector4 ToVector4(this Box2 box) => new(box.Min.X, box.Min.Y, box.Max.X, box.Max.Y);

    public static Vector2i ToVector2i(this Vector2 v) => new((int)v.X, (int)v.Y);
    public static Vector3i ToVector3i(this Vector3 v) => new((int)v.X, (int)v.Y, (int)v.Z);
    public static Vector4i ToVector4i(this Vector4 v) => new((int)v.X, (int)v.Y, (int)v.Z, (int)v.W);

    public static Vector2 GetTopLeft(this Box2 rectangle) => new(rectangle.Min.X, rectangle.Min.Y);
    public static Vector2 GetBottomLeft(this Box2 rectangle) => new(rectangle.Min.X, rectangle.Max.Y);
    public static Vector2 GetBottomRight(this Box2 rectangle) => new(rectangle.Max.X, rectangle.Max.Y);
    public static Vector2 GetTopRight(this Box2 rectangle) => new(rectangle.Max.X, rectangle.Min.Y);

    public static Vector4 ToVector(this Color4 color) => new(color.R, color.G, color.B, color.A);
    public static Color4 ToColor4(this Vector4 color) => new(color.X, color.Y, color.Z, color.W);

    public static Vector2 Reflected(this Vector2 vector, Vector2 normal)
    {
        return vector * (Vector2.One - normal.Abs()) + vector * -normal.Abs();
    }

    public static Vector2 GetNormal(this Face2 face, Vector2 @default = default) => face switch
    {
        Face2.Left => -Vector2.UnitX,
        Face2.Right => Vector2.UnitX,
        Face2.Top => -Vector2.UnitY,
        Face2.Bottom => Vector2.UnitY,
        _ => @default
    };

    public static Vector3 GetNormal(this Face3 face, Vector3 @default = default) => face switch
    {
        Face3.Left => -Vector3.UnitX,
        Face3.Right => Vector3.UnitX,
        Face3.Top => Vector3.UnitY,
        Face3.Bottom => -Vector3.UnitY,
        Face3.Back => -Vector3.UnitZ,
        Face3.Front => Vector3.UnitZ,
        _ => @default
    };

    public static Box2 ActuallyInflated(this Box2 box, Vector2 size)
    {
        size = Vector2.ComponentMax(size, -box.HalfSize);

        return new Box2(box.Min - size, box.Max + size);
    }

    public static Box3 ActuallyInflated(this Box3 box, Vector3 size)
    {
        size = Vector3.ComponentMax(size, -box.HalfSize);

        return new Box3(box.Min - size, box.Max + size);
    }

}
