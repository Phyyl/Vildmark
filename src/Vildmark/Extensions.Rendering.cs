using OpenTK.Mathematics;
using Vildmark.Graphics;
using Vildmark.Graphics.Fonts.Msdf;
using Vildmark.Graphics.Rendering;

namespace Vildmark;

public static partial class Extensions
{
    public static void RenderString(this Renderer renderer, MsdfFont font, string text, float size, Color4 foreground, Color4? background = default, Transform? transform = default)
    {
        font.RenderString(renderer, text, size, foreground, background, transform);
    }
}
