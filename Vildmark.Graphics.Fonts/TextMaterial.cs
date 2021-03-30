using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Rendering;

namespace Vildmark.Graphics.Fonts
{
    public class TextMaterial
    {
        public GLTexture2D[] Pages { get; init; }

        public Vector4 Tint { get; set; }

        public TextMaterial(params GLTexture2D[] pages)
        {
            Pages = pages;
        }

        public static implicit operator TextMaterial(GLTexture2D[] pages) => new(pages);
    }
}
