using OpenTK.Mathematics;

namespace Vildmark.Graphics.Fonts;

internal record struct FontGlyph(char Character, Box2 PlaneBounds, Box2 AtlasBounds, float Advance);
