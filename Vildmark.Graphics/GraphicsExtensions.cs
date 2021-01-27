using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vildmark.Graphics
{
    public static class GraphicsExtensions
    {
        public static Vector2 GetTopLeft(this RectangleF rectangle) => new Vector2(rectangle.Left, rectangle.Top);
        public static Vector2 GetBottomLeft(this RectangleF rectangle) => new Vector2(rectangle.Left, rectangle.Bottom);
        public static Vector2 GetBottomRight(this RectangleF rectangle) => new Vector2(rectangle.Right, rectangle.Bottom);
        public static Vector2 GetTopRight(this RectangleF rectangle) => new Vector2(rectangle.Right, rectangle.Top);
    }
}
