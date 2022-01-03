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
using Vildmark.Graphics.Shaders;
using Vildmark.Maths;

namespace Vildmark.Graphics.Rendering
{
    public class TexturedMaterial
    {
        public Texture2D? Texture { get; set; }
        public Color4 Color { get; set; }
    }

    public class TexturedModel : Model<Vertex, TexturedMaterial, TexturedShader> { }
    public class TexturedShader : EmbeddedShader<Vertex, TexturedMaterial>
    {
        public TexturedShader() : base("model") { }

        protected override void SetupUniforms(TexturedMaterial material, Camera camera, Transform transform)
        {
            Texture2D texture = material.Texture ?? Texture2D.WhitePixel;

            Uniform("projection_matrix", camera.ProjectionMatrix);
            Uniform("view_matrix", camera.ViewMatrix);
            Uniform("model_matrix", transform);
            Uniform("source_rect", texture.SourceRectangle.ToVector());
            Uniform("tex", texture);
            Uniform("tint", material.Color);
        }

        protected override void SetupAttribs()
        {
            AttribPointer("vert_position", nameof(Vertex.Position));
            AttribPointer("vert_color", nameof(Vertex.Color));
            AttribPointer("vert_texcoord", nameof(Vertex.TexCoord));
            AttribPointer("vert_normal", nameof(Vertex.Normal));
        }
    }
}
