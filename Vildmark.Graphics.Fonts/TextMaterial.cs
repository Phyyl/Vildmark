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
    public class TextMaterial : ColorMaterial
    {
        public GLTexture2D[] Pages { get; init; }

        public TextMaterial(Vector4 tint, params GLTexture2D[] pages)
            : base(tint)
        {
            Pages = pages;
        }

        public TextMaterial(params GLTexture2D[] pages)
            : this(Vector4.One, pages)
        {
        }

        public static implicit operator TextMaterial(GLTexture2D[] pages) => new(pages);

        public override void SetupShader(IShader shader)
        {
            base.SetupShader(shader);

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
