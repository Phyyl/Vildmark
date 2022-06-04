using OpenTK.Mathematics;
using System.Drawing;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Fonts.Msdf;

public class MsdfText
{
    private bool needsUpdate = true;

    private readonly Mesh<Vertex> mesh = new();
    private readonly MsdfFont font;
    private string text;
    private float fontSize;
    private float maxLineLength;

    public RectangleF Bounds { get; private set; }

    public Vector2 Position => Bounds.Location.ToVector();
    public Vector2 Size => Bounds.Size.ToVector();

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

    public MsdfText(MsdfFont font, string text, float fontSize, float maxLineLength = float.PositiveInfinity)
    {
        this.font = font;
        this.text = text;
        this.fontSize = fontSize;
        this.maxLineLength = maxLineLength;
    }

    public void Render(Renderer renderer, Color4 foreground, Color4? background = default, Transform? transform = default)
    {
        if (needsUpdate)
        {
            UpdateMesh();
            needsUpdate = false;
        }

        renderer.Render(mesh, new MsdfMaterial(font.Texture, foreground, background ?? foreground with { A = 0 }, font.Info.Atlas?.DistanceRange ?? 2), MsdfFont.Shader, transform);
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
