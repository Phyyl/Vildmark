using OpenTK.Mathematics;
using System.Drawing;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Textures;
using Vildmark.Maths;

namespace Vildmark.Graphics.Shaders
{
    public class TexturedShader : EmbeddedShader, IShaderSetup<Camera>, IShaderMaterialSetup, IShaderSetup<Transform?>
    {
        private static readonly Vector2 texCoordCorrection = new(0.00000002f);

        public TexturedShader()
            : base("model")
        {
        }

        void IShaderMaterialSetup.Setup<TMaterial>(TMaterial input)
        {
            Texture2D texture = Texture2D.WhitePixel;
            Color4 tint = Color4.White;

            if (input is ITextureMaterial textureMaterial)
            {
                texture = textureMaterial.Texture ?? Texture2D.TransparentPixel;
            }
            else if (input is Texture2D texture2D)
            {
                texture = texture2D;
            }
            else if (input is GLTexture2D glTexture2D)
            {
                texture = glTexture2D;
            }

            if (input is IColorMaterial colorMaterial)
            {
                tint = colorMaterial.Color;
            }
            else if (input is Color4 color4)
            {
                tint = color4;
            }

            Vector2 texelSize = new(1f / texture.GLTexture.Width, 1f / texture.GLTexture.Height);

            Uniform("source_rect", texture.SourceRectangle.Translated(texCoordCorrection).Inflated(-texCoordCorrection * 2));
            Uniform("tex", texture);
            Uniform("texel_size", texelSize);
            Uniform("tint", tint);

        }

        void IShaderSetup<Camera>.Setup(Camera input)
        {
            Uniform("projection_matrix", input.ProjectionMatrix);
            Uniform("view_matrix", input.ViewMatrix);
        }

        void IShaderSetup<Transform?>.Setup(Transform? input)
        {
            Uniform("model_matrix", input);
        }
    }
}
