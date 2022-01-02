using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Fonts
{
    public record struct TextMaterial(Texture2D[] Textures, Color4 Tint);
    public class TextModel : Model<TextVertex, TextMaterial, TextShader> { }
    public class TextShader : EmbeddedShader<TextVertex, TextMaterial>
    {
        public TextShader()
            : base("text")
        {
        }

        protected override void SetupAttribs()
        {
            AttribPointer("vert_position", nameof(TextVertex.Position));
            AttribPointer("vert_texcoord", nameof(TextVertex.TexCoord));
            AttribPointer("vert_texture_index", nameof(TextVertex.TextureIndex));
        }

        protected override void SetupUniforms(TextMaterial material, Camera camera, Transform transform)
        {
            Uniform("tint", material.Tint);
            Uniform("textures", material.Textures);
            Uniform("projection_matrix", camera.ProjectionMatrix);
            Uniform("view_matrix", camera.ViewMatrix);
            Uniform("model_matrix", transform.Matrix);
        }
    }
}
