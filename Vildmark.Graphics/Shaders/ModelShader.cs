using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;
using Vildmark.Maths;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
    public class ModelShader : Shader, IVertexShader, IModelViewProjectionMatrixShader, ITintShader, ITextureShader, ISourceRectShader, IOffsetShader
    {
        protected override string VertexShaderSource => ResourceLoader.LoadEmbedded<string>("texture.vert");
        protected override string FragmentShaderSource => ResourceLoader.LoadEmbedded<string>("texture.frag");

        public Attrib<Vector3> Position { get; } = new Attrib<Vector3>("vert_position");
        public Attrib<Vector2> TexCoord { get; } = new Attrib<Vector2>("vert_tex_coord");
        public Attrib<Vector4> Color { get; } = new Attrib<Vector4>("vert_color");
        public Attrib<Vector3> Normal { get; } = new Attrib<Vector3>("vert_normal");

        public Uniform<Matrix4> ProjectionMatrix { get; } = new Uniform<Matrix4>("projection_matrix");
        public Uniform<Matrix4> ViewMatrix { get; } = new Uniform<Matrix4>("view_matrix");
        public Uniform<Matrix4> ModelMatrix { get; } = new Uniform<Matrix4>("model_matrix");

        public Uniform<Vector4> Tint { get; } = new Uniform<Vector4>("tint");

        public Uniform<Vector4> SourceRect { get; } = new Uniform<Vector4>("source_rect");
        public Uniform<Sampler2D> Texture { get; } = new Uniform<Sampler2D>("texture");

        public Uniform<Vector3> Offset { get; } = new Uniform<Vector3>("offset");
    }
}
