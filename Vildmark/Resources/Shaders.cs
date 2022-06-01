using Vildmark.Graphics.Fonts;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Resources;

internal static class Shaders
{
    public static BitmapFontShader BitmapFontShader { get; } = new();
    public static TexturedShader TexturedShader { get; } = new();
}
