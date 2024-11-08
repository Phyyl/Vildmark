using OpenTK.Mathematics;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Fonts;

public class FontText(Font font, string text, float fontSize, float maxLineLength = float.PositiveInfinity)
{
    private bool needsUpdate = true;

    private readonly Mesh<Vertex> mesh = new();

    public Box2 Bounds { get; private set; }

    public string Text
    {
        get => text;
        set => SetValue(ref text, value);
    }

    public float FontSize
    {
        get => fontSize;
        set => SetValue(ref fontSize, value);
    }

    public float MaxLineLength
    {
        get => maxLineLength;
        set => SetValue(ref maxLineLength, value);
    }

    public void Render(Renderer renderer, Color4<Rgba> foreground, Color4<Rgba>? background = default, Transform? transform = default)
    {
        if (needsUpdate)
        {
            UpdateMesh();
            needsUpdate = false;
        }

        renderer.Render(mesh, new FontMaterial(font.Texture, foreground, background ?? foreground with { W = 0 }, 1.75f), Font.Shader, transform);
    }

    private void UpdateMesh()
    {
        mesh.UpdateVertices(font.CreateMesh(text, fontSize, MaxLineLength));
        Bounds = font.MeasureString(text, fontSize, MaxLineLength);
    }

    private void SetValue<T>(ref T field, T value)
    {
        field = value;
        needsUpdate = true;
    }
}
