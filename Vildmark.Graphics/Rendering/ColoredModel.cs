using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Rendering
{
    public class ColoredModel : Model<Vector2, Color4, ColoredShader> { }
    public class ColoredShader : EmbeddedShader<Vector2, Color4>
    {
        public ColoredShader() : base("colored") { }

        protected override void SetupUniforms(Color4 material, Camera camera, Transform transform)
        {
            Uniform("color", material);
            Uniform("projection_matrix", camera.ProjectionMatrix);
            Uniform("view_matrix", camera.ViewMatrix);
            Uniform("model_matrix", transform);
        }

        protected unsafe override void SetupAttribs()
        {
            AttribPointer<Vector2>("vert_position", sizeof(Vector2), 0);
        }
    }
}
