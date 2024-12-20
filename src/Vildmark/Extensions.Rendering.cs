﻿using OpenTK.Mathematics;
using Vildmark.Graphics;
using Vildmark.Graphics.Fonts;
using Vildmark.Graphics.Rendering;

namespace Vildmark;

public static partial class Extensions
{
    public static void RenderString(this Renderer renderer, Font font, string text, float size, Color4<Rgba> foreground, Color4<Rgba>? background = default, Transform? transform = default)
    {
        font.RenderString(renderer, text, size, foreground, background, transform);
    }
}
