using OpenTK.Mathematics;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.Fonts;

public record FontMaterial(Texture2D Texture, Color4<Rgba> ForegroundColor, Color4<Rgba> BackgroundColor, float PxRange);
