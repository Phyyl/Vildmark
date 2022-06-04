using OpenTK.Mathematics;

namespace Vildmark.Graphics.Fonts.Msdf;

internal record struct MsdfGlyph(char Character, Box2 PlaneBounds, Box2 AtlasBounds, float Advance);
