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

        public void RenderTexture(Texture2D texture, Vector2 position, Vector2 size) => RenderTexture(texture, position, size, Color4.White);
        public void RenderTexture(Texture2D texture, Vector2 position, Vector2 size, Color4 tint)
        {
            texturedModel.Material.Color = tint;
            texturedModel.Material.Texture = texture;

            texturedModel.Mesh.UpdateVertices(new Vertex[]
            {
                new Vertex(position, Vector2.Zero),
                new Vertex(position + size * Vector2.UnitY, Vector2.UnitY),
                new Vertex(position + size, Vector2.One),
                new Vertex(position, Vector2.Zero),
                new Vertex(position + size, Vector2.One),
                new Vertex(position + size * Vector2.UnitX, Vector2.UnitX)
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
