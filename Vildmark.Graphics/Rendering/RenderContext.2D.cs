using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Models;
using Vildmark.Maths;

namespace Vildmark.Graphics.Rendering
{
    public partial class RenderContext
    {
        private ColoredModel colorModel = new();

        public void RenderRectangle(RectangleF rectangle, Color4 color)
        {
            Render(new Vector2[]
            {
                rectangle.GetTopLeft(),
                rectangle.GetBottomLeft(),
                rectangle.GetBottomRight(),
                rectangle.GetTopLeft(),
                rectangle.GetBottomRight(),
                rectangle.GetTopRight(),
            }, color);
        }

        public void RenderCircle(Vector2 center, float radius, Color4 color, int sides = 0)
        {
            if (sides <= 0)
            {
                sides = (int)(MathF.Sqrt(radius * 12) + 12f);
            }

            float angleStep = 1f / sides * MathF.Tau;

            Vector2[] vertices = new Vector2[sides + 2];

            vertices[0] = center;

            for (int i = 0; i <= sides; i++)
            {
                vertices[i + 1] = center + new Vector2(MathF.Cos(angleStep * i), MathF.Sin(angleStep * i)) * radius;
            }

            Render(vertices, color, PrimitiveType.TriangleFan);
        }

        public virtual void Render(Span<Vector2> vertices, Color4 color, PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            colorModel.Mesh.UpdateVertices(vertices);
            colorModel.Material = color;
            colorModel.Render(this, primitiveType);
        }
    }
}
