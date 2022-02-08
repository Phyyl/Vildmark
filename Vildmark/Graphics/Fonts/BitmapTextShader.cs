using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Fonts
{
    public class BitmapFontShader : EmbeddedShader, IShaderSetup<Camera>, IShaderMaterialSetup, IShaderSetup<Transform>
    {
        public BitmapFontShader()
            : base("bitmapfont")
        {
        }

        void IShaderMaterialSetup.Setup<TMaterial>(TMaterial input)
        {
            if (input is IColorMaterial colorMaterial)
            {
                Uniform("tint", colorMaterial.Color);
            }

            if (input is ITextureMaterial textureMaterial)
            {
                Uniform("textures", textureMaterial.Texture);
            }

            if (input is ITexturesMaterial texturesMaterial)
            {
                Uniform("textures", texturesMaterial.Textures);
            }
        }

        void IShaderSetup<Transform>.Setup(Transform input)
        {
            // TODO: Try implementing position, offset, scale and as much as possible via uniforms instead of Tranform.Matrix to save CPU time
            Uniform("model_matrix", input.Matrix);
        }

        void IShaderSetup<Camera>.Setup(Camera input)
        {
            Uniform("projection_matrix", input.ProjectionMatrix);
            Uniform("view_matrix", input.ViewMatrix);
        }
    }
}
