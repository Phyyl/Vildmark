using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
    public class ShadowShader : Shader, IModelViewProjectionMatrixShader, IOffsetShader, IPositionShader
    {
        protected override string VertexShaderSource => ResourceLoader.LoadEmbedded<string>("shadow.vert");
        protected override string FragmentShaderSource => ResourceLoader.LoadEmbedded<string>("shadow.frag");

        public Uniform<Matrix4> ProjectionMatrix { get; } = new Uniform<Matrix4>("projection_matrix");
        public Uniform<Matrix4> ViewMatrix { get; } = new Uniform<Matrix4>("view_matrix");
        public Uniform<Matrix4> ModelMatrix { get; } = new Uniform<Matrix4>("model_matrix");
        public Uniform<Vector3> Offset { get; } = new Uniform<Vector3>("offset");
        public Attrib<Vector3> Position { get; } = new Attrib<Vector3>("vert_position");
    }
}
