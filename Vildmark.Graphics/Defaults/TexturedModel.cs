using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Defaults
{
    public record class TexturedMaterial(Texture2D Texture, Color4 Color);

    internal class TexturedModel : Model<Vertex, TexturedMaterial, TexturedShader>
    {
        public TexturedModel(TexturedMaterial material) : base(material) { }

        protected override void SetupAttribs()
        {
            AttribPointer("vert_position", "Position");
            AttribPointer("vert_color", "Color");
            AttribPointer("vert_texcoord", "TexCoord");
            AttribPointer("vert_normal", "Normal");
        }
    }

    public class TexturedShader : EmbeddedShader<Vertex, TexturedMaterial>
    {
        public TexturedShader() : base("model") { }

        protected override void Setup(TexturedMaterial material, Camera camera, Transform transform)
        {
            Uniform("tint", material.Color);
            Uniform("tex", material.Texture);
            Uniform("projection_matrix", camera.ProjectionMatrix);
            Uniform("view_matrix", camera.ViewMatrix);
            Uniform("model_matrix", transform);
        }
    }

}
