using OpenTK.Mathematics;
using System.Drawing;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Materials;
using Vildmark.Graphics.Meshes;
using Vildmark.Resources;

namespace Vildmark.Graphics.Shaders
{
    public class TexturedShader : Shader<Vertex, TexturedMaterial>
    {
        public TexturedShader()
            : base(ResourceLoader.LoadEmbedded<GLShaderProgram>("textured"))
        {
        }

        public Attrib<Vector3> PositionAttrib { get; } = new("vert_position");
        public Attrib<Vector4> ColorAttrib { get; } = new("vert_color");
        public Attrib<Vector2> TexCoordAttrib { get; } = new("vert_tex_coord");
        public Attrib<Vector3> NormalAttrib { get; } = new("vert_normal");

        public Uniform<Matrix4> ProjectionMatrix { get; } = new("projection_matrix");
        public Uniform<Matrix4> ViewMatrix { get; } = new("view_matrix");
        public Uniform<Matrix4> ModelMatrix { get; } = new("model_matrix");
        public Uniform<GLTexture2D> Texture { get; } = new("tex");
        public Uniform<Color4> Tint { get; } = new("tint");
        public Uniform<RectangleF> SourceRect { get; } = new("source_rect");

        public override void Setup(Mesh<Vertex> mesh, TexturedMaterial material, Camera camera, Transform? transform = null)
        {
            Use();

            mesh.VertexArray.Bind();
            mesh.VertexBuffer.Bind();

            PositionAttrib.VertexAttribPointer<Vertex>(Vertex.PositionOffset);
            ColorAttrib.VertexAttribPointer<Vertex>(Vertex.ColorOffset);
            TexCoordAttrib.VertexAttribPointer<Vertex>(Vertex.TexCoordOffset);
            NormalAttrib.VertexAttribPointer<Vertex>(Vertex.NormalOffset);

            ProjectionMatrix.SetUniform(camera.ProjectionMatrix);
            ViewMatrix.SetUniform(camera.ViewMatrix);
            ModelMatrix.SetUniform(transform);

            Texture.SetUniform(material.Texture.GLTexture);
            SourceRect.SetUniform(material.Texture.SourceRectangle);
            Tint.SetUniform(material.Tint);
        }
    }
}
