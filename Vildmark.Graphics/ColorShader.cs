using OpenTK.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Shaders;
using Vildmark.Resources;

namespace Vildmark.Graphics.Resources
{
    public class ColorShader : Shader,
        IMeshShader<Mesh<ColorVertex>>,
        IMaterialShader<ColorMaterial>,
        IModelMatrixShader,
        ICameraShader
    {
        protected override string VertexShaderSource => ResourceLoader.LoadEmbedded<string>("color.vert");
        protected override string FragmentShaderSource => ResourceLoader.LoadEmbedded<string>("color.frag");

        public Uniform<Matrix4> ProjectionMatrix { get; } = new Uniform<Matrix4>("projection_matrix");
        public Uniform<Matrix4> ViewMatrix { get; } = new Uniform<Matrix4>("view_matrix");
        public Uniform<Matrix4> ModelMatrix { get; } = new Uniform<Matrix4>("model_matrix");
        public Uniform<Vector4> Tint { get; } = new Uniform<Vector4>("tint");

        public Attrib<Vector3> Position { get; } = new Attrib<Vector3>("vert_position");
        public Attrib<Vector4> Color { get; } = new Attrib<Vector4>("vert_color");

        public void SetupMesh(Mesh<ColorVertex> mesh)
        {
            if (mesh is null)
            {
                return;
            }

            if (mesh.VertexArray is null || mesh.VertexBuffer is null)
            {
                return;
            }

            mesh.VertexArray.Bind();

            Position.Setup(mesh.VertexBuffer, ColorVertex.PositionOffset);
            Color.Setup(mesh.VertexBuffer, ColorVertex.ColorOffset);
        }

        public void SetupMaterial(ColorMaterial material)
        {
            if (material is null)
            {
                return;
            }

            Tint.SetValue(material.Tint);
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

        public void SetupModelMatrix(Matrix4 modelMatrix)
        {
            ModelMatrix.SetValue(modelMatrix);
        }
    }
}
