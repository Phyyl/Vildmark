using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Rendering;

public partial class Renderer
{
    private readonly Mesh<Vertex> mesh = new();
    private readonly TexturedShader texturedShader = new();

    public void RenderRectangle(Box2 rectangle, TexturedMaterial material, Transform? transform = default)
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

        Render(mesh, material, texturedShader, transform);
    }

    public void RenderCircle(Vector2 center, float radius, TexturedMaterial material, int sides = 0, Transform? transform = default)
    {
        sides = Math.Max(sides, (int)(MathF.Round((MathF.Sqrt(radius * 12) + 12f) / 4f) * 4f));

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

        Render(mesh, material, texturedShader, transform, PrimitiveType.TriangleFan);
    }
}
