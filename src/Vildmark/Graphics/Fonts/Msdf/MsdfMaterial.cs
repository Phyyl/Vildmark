using OpenTK.Mathematics;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.Fonts.Msdf;

public record MsdfMaterial(Texture2D Texture, Color4 ForegroundColor, Color4 BackgroundColor, float PxRange);
