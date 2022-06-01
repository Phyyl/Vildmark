using OpenTK.Mathematics;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Fonts;

public static class BitmapFontExtensions
{
    public static void RenderString(this RenderContext renderContext, BitmapFont bitmapFont, string text, Vector2 position, float size, Color4 color) => bitmapFont.RenderString(renderContext, text, position, size, color);
}
