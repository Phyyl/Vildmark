using OpenTK.Mathematics;
using System.Drawing;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Textures;

namespace Vildmark.Graphics.Shaders
{
    public class TexturedShader : EmbeddedShader, IShaderSetup<Camera>, IShaderSetup<IMaterial>, IShaderSetup<Transform?>
    {
        public TexturedShader()
            : base("model")
        {
        }

        void IShaderSetup<IMaterial>.Setup(IMaterial input)
        {
            if (input is ITextureMaterial textureMaterial)
            {
                Texture2D texture = textureMaterial.Texture ?? Texture2D.TransparentPixel;
                Vector2 texelSize = new(1f / texture.GLTexture.Width, 1f / texture.GLTexture.Height);

                Uniform("source_rect", texture.SourceRectangle);
                Uniform("tex", texture);
                Uniform("texel_size", texelSize);
            }
            else
            {
                Uniform("source_rect", new RectangleF(0, 0, 1, 1));
                Uniform("tex", Texture2D.WhitePixel);
            }

            if (input is IColorMaterial colorMaterial)
            {
                Uniform("tint", colorMaterial.Color);
            }
            else
            {
                Uniform("tint", Color4.White);
            }
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
