using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Fonts
{
    public class TextMaterial : IMaterial
    {
        public GLTexture2D[] Pages { get; init; }

        public Vector4 Tint { get; set; }

        public TextMaterial(params GLTexture2D[] pages)
        {
            Pages = pages;
        }

        public static implicit operator TextMaterial(GLTexture2D[] pages) => new(pages);

        public void SetupShader(IShader shader)
        {
            if (shader is ITintShader tintShader)
            {
                tintShader.Tint.SetValue(Tint);
            }

            if (shader is ITexturesShader texturesShader)
            {
                for (int i = 0; i < Pages.Length && i < texturesShader.MaxTextures; i++)
                {
                    texturesShader.Textures.SetValue(Pages[i], i);
                }
            }
        }
    }
}
