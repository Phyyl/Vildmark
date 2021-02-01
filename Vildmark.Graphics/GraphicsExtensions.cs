using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Maths;

namespace Vildmark.Graphics
{
    public static class GraphicsExtensions
    {
        public static Vector2 GetTopLeft(this RectangleF rectangle) => new Vector2(rectangle.Left, rectangle.Top);
        public static Vector2 GetBottomLeft(this RectangleF rectangle) => new Vector2(rectangle.Left, rectangle.Bottom);
        public static Vector2 GetBottomRight(this RectangleF rectangle) => new Vector2(rectangle.Right, rectangle.Bottom);
        public static Vector2 GetTopRight(this RectangleF rectangle) => new Vector2(rectangle.Right, rectangle.Top);

        public static Vector2 ToVector(this SizeF size) => new Vector2(size.Width, size.Height);
        public static Vector2 ToVector(this PointF size) => new Vector2(size.X, size.Y);

        public static Vector4 ToVector(this Color4 color) => new Vector4(color.R, color.G, color.B, color.A);

        public static Color4 ToColor4(this Vector4 color) => new Color4(color.X, color.Y, color.Z, color.W);
    }
}
