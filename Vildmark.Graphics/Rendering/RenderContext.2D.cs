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
        private ColoredModel coloredModel = new();
        private TexturedModel texturedModel = new();

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

        public void RenderTexture(Texture2D texture, Vector2 position, Vector2 size, Color4? tint = default, bool flipX = false, bool flipY = false)
        {
            texturedModel.Material.Color = tint ?? Color4.White;
            texturedModel.Material.Texture = texture;

            Vector2 topLeft = new(0, 0);
            Vector2 bottomLeft = new(0, 1);
            Vector2 bottomRight = new(1, 1);
            Vector2 topRight = new(1, 0);

            if (flipX)
            {
                (topLeft, bottomLeft, bottomRight, topRight) = (topRight, bottomRight, bottomLeft, topLeft);
            }

            if (flipY)
            {
                (topLeft, bottomLeft, bottomRight, topRight) = (bottomLeft, topLeft, topRight, bottomRight);
            }

            texturedModel.Mesh.UpdateVertices(new Vertex[]
            {
                new Vertex(position, topLeft),
                new Vertex(position + size * Vector2.UnitY, bottomLeft),
                new Vertex(position + size, bottomRight),
                new Vertex(position, topLeft),
                new Vertex(position + size, bottomRight),
                new Vertex(position + size * Vector2.UnitX, topRight)
            });

            texturedModel.Render(this);
        }

        public virtual void Render(Span<Vector2> vertices, Color4 color, PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            coloredModel.Mesh.UpdateVertices(vertices);
            coloredModel.Material = color;
            coloredModel.Render(this, primitiveType);
        }
    }
}
