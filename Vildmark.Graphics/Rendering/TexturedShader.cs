using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Shaders;
using Vildmark.Maths;

namespace Vildmark.Graphics.Rendering
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
                Texture2D texture = textureMaterial.Texture ?? Texture2D.WhitePixel;
                Uniform("source_rect", texture.SourceRectangle.ToVector());
                Uniform("tex", texture);
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
