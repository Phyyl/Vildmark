using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Fonts
{
    public record class TextMaterial
    {
        public Texture2D[] Textures { get; init; } = new Texture2D[8];

        public Color4 Tint { get; init; } = Color4.White;
    }
}
