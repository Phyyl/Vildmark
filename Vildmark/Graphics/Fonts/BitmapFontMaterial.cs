using OpenTK.Mathematics;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Graphics.Fonts;

public record BitmapFontMaterial(GLTexture2D[] Textures, Color4 Color);
