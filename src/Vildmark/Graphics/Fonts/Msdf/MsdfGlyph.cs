using System.Drawing;

namespace Vildmark.Graphics.Fonts.Msdf;

internal record struct MsdfGlyph(char Character, RectangleF PlaneBounds, RectangleF AtlasBounds, float Advance);
