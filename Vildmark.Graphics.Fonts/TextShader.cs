using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Meshes;
using Vildmark.Graphics.Rendering;
using Vildmark.Graphics.Shaders;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts
{
    public class TextShader : EmbeddedShader<TextVertex, TextMaterial>
    {
        public TextShader()
            : base("text")
        {
        }

        protected override void Setup(TextMaterial material, Camera camera, Transform transform)
        {
            Uniform("tint", material.Tint);
            Uniform("textures", material.Textures);
            Uniform("projection_matrix", camera.ProjectionMatrix);
            Uniform("view_matrix", camera.ViewMatrix);
            Uniform("model_matrix", transform.Matrix);
        }
    }
}
