using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Materials;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
    public class ColorShader : Shader, IColorShader, IPositionShader, IModelViewProjectionMatrixShader, ITintShader
    {
        protected override string VertexShaderSource => ResourceLoader.LoadEmbedded<string>("color.vert");
        protected override string FragmentShaderSource => ResourceLoader.LoadEmbedded<string>("color.frag");

        public Uniform<Matrix4> ProjectionMatrix { get; } = new Uniform<Matrix4>("projection_matrix");
        public Uniform<Matrix4> ViewMatrix { get; } = new Uniform<Matrix4>("view_matrix");
        public Uniform<Matrix4> ModelMatrix { get; } = new Uniform<Matrix4>("model_matrix");
        public Uniform<Vector4> Tint { get; } = new Uniform<Vector4>("tint");

        public Attrib<Vector3> Position { get; } = new Attrib<Vector3>("vert_position");
        public Attrib<Vector4> Color { get; } = new Attrib<Vector4>("vert_color");
    }
}
