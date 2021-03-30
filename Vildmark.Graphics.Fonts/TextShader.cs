using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.Shaders;
using Vildmark.Resources;

namespace Vildmark.Graphics.Fonts
{
    public class TextShader : Shader,
        IMaterialShader<TextMaterial>,
        IMeshShader<Mesh<TextVertex>>,
        IModelMatrixShader,
        ICameraShader
    {
        private const int maxTextures = 8;

        protected override string VertexShaderSource => ResourceLoader.LoadEmbedded<string>("text.vert");
        protected override string FragmentShaderSource => ResourceLoader.LoadEmbedded<string>("text.frag");

        public Attrib<Vector3> Position { get; } = new Attrib<Vector3>("vert_position");
        public Attrib<Vector2> TexCoord { get; } = new Attrib<Vector2>("vert_tex_coord");
        public Attrib<int> TextureIndex { get; } = new Attrib<int>("texture_index");
        public Uniform<Matrix4> ProjectionMatrix { get; } = new Uniform<Matrix4>("projection_matrix");
        public Uniform<Matrix4> ViewMatrix { get; } = new Uniform<Matrix4>("view_matrix");
        public Uniform<Matrix4> ModelMatrix { get; } = new Uniform<Matrix4>("model_matrix");
        public Uniform<Vector4> Tint { get; } = new Uniform<Vector4>("tint");
        public IndexedUniform<Sampler2D> Texture { get; } = new IndexedUniform<Sampler2D>("textures");

        public void SetupCamera(Camera camera)
        {
            if (camera is null)
            {
                return;
            }

            ProjectionMatrix.SetValue(camera.ProjectionMatrix);
            ViewMatrix.SetValue(camera.ViewMatrix);
        }

        public void SetupMaterial(TextMaterial material)
        {
            for (int i = 0; i < material.Pages.Length && i < maxTextures; i++)
            {
                Texture.SetValue(i, material.Pages[i]);
            }

            Tint.SetValue(material.Tint);
        }

        public void SetupMesh(Mesh<TextVertex> mesh)
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

            Position.Setup(mesh.VertexBuffer, TextVertex.PositionOffset);
            TexCoord.Setup(mesh.VertexBuffer, TextVertex.TexCoordOffset);
            TextureIndex.Setup(mesh.VertexBuffer, TextVertex.TextureIndexOffset);
        }

        public void SetupModelMatrix(Matrix4 modelMatrix)
        {
            ModelMatrix.SetValue(modelMatrix);
        }
    }
}
