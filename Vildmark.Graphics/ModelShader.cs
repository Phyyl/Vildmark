using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Shaders;
using Vildmark.Maths;
using Vildmark.Resources;

namespace Vildmark.Graphics.Resources
{
    public class ModelShader : Shader,
        IMeshShader<Mesh<Vertex>>,
        ICameraShader,
        IModelMatrixShader,
        IMaterialShader<TextureMaterial>
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
        
        public void SetupMesh(Mesh<Vertex> mesh)
        {
            if (mesh is null || mesh.VertexArray is null || mesh.VertexBuffer is null)
            {
                return;
            }

            mesh.VertexArray.Bind();

            Position.Setup(mesh.VertexBuffer, Vertex.PositionOffset);
            TexCoord.Setup(mesh.VertexBuffer, Vertex.TexCoordOffset);
            Normal.Setup(mesh.VertexBuffer, Vertex.NormalOffset);
            Color.Setup(mesh.VertexBuffer, Vertex.ColorOffset);
        }

        public void SetupCamera(Camera camera)
        {
            if (camera is null)
            {
                return;
            }

            ProjectionMatrix.SetValue(camera.ProjectionMatrix);
            ViewMatrix.SetValue(camera.ViewMatrix);
        }

        public void SetupMaterial(TextureMaterial material)
        {
            if (material is null)
            {
                return;
            }

            if (material.Texture is not null)
            {
                Texture.SetValue(material.Texture.GLTexture);
                SourceRect.SetValue(material.Texture.SourceRectangle.ToVector());
            }

            Tint.SetValue(material.Tint);
        }

        public void SetupModelMatrix(Matrix4 modelMatrix)
        {
            ModelMatrix.SetValue(modelMatrix);
        }
    }
}
