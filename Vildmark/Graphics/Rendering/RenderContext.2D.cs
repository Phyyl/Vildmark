using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Shaders;
using Vildmark.Maths;

namespace Vildmark.Graphics.Rendering
{
    public partial class RenderContext
    {
        private Mesh mesh = new();

        public void RenderRectangle(RectangleF rectangle, Color4 color, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.Triangles, IShader? shader = default) => RenderRectangle(rectangle, new ColorMaterial(color), transform, primitiveType, shader);
        public void RenderRectangle(RectangleF rectangle, Texture2D texture, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.Triangles, IShader? shader = default) => RenderRectangle(rectangle, new TextureMaterial(texture), transform, primitiveType, shader);
        public void RenderRectangle<TMaterial>(RectangleF rectangle, TMaterial material, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.Triangles, IShader? shader = default)
            where TMaterial : IMaterial
        {
            mesh.UpdateVertices(new Vertex[]
            {
                new(rectangle.GetTopLeft(), Vector2.Zero),
                new(rectangle.GetBottomLeft(), Vector2.UnitY),
                new(rectangle.GetBottomRight(), Vector2.One),
                new(rectangle.GetTopLeft(), Vector2.Zero),
                new(rectangle.GetBottomRight(), Vector2.One),
                new(rectangle.GetTopRight(), Vector2.UnitX),
            });

            Render(mesh, material, transform, primitiveType, shader);
        }

        public void RenderCircle(Vector2 center, float radius, Color4 color, int sides = 0, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.TriangleFan, IShader? shader = default) => RenderCircle(center, radius, new ColorMaterial(color), sides, transform, primitiveType, shader);
        public void RenderCircle(Vector2 center, float radius, Texture2D texture, int sides = 0, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.TriangleFan, IShader? shader = default) => RenderCircle(center, radius, new TextureMaterial(texture), sides, transform, primitiveType, shader);
        public void RenderCircle<TMaterial>(Vector2 center, float radius, TMaterial material, int sides = 0, Transform? transform = default, PrimitiveType primitiveType = PrimitiveType.TriangleFan, IShader? shader = default)
            where TMaterial : IMaterial
        {
            if (sides <= 0)
            {
                sides = (int)(MathF.Sqrt(radius * 12) + 12f);
            }

            float angleStep = 1f / sides * MathF.Tau;
            Vertex[] vertices = new Vertex[sides + 2];

            vertices[0] = new Vertex(center, new Vector2(0.5f, 0.5f));

            for (int i = 0; i <= sides; i++)
            {
                float cos = MathF.Cos(angleStep * i);
                float sin = MathF.Sin(angleStep * i);

                vertices[i + 1] = new Vertex(center + new Vector2(cos, sin) * radius, new Vector2(cos, sin) / 2f + new Vector2(0.5f, 0.5f));
            }

            mesh.UpdateVertices(vertices);

            Render(mesh, material, transform, primitiveType, shader);
        }
    }
}
