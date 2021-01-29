using OpenTK.Mathematics;
using System;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Shaders;
using Vildmark.Maths;

namespace Vildmark.Graphics.Resources
{
    public class MaterialShader : Shader
    {
        public Attrib<Vector3> Position { get; } = new Attrib<Vector3>("vert_position");

        public Attrib<Vector2> TexCoord { get; } = new Attrib<Vector2>("vert_tex_coord");

        public Attrib<Vector4> Color { get; } = new Attrib<Vector4>("vert_color");

        public Attrib<Vector3> Normal { get; } = new Attrib<Vector3>("vert_normal");

        public Uniform<Matrix4> ProjectionMatrix { get; } = new Uniform<Matrix4>("projection_matrix");

        public Uniform<Matrix4> ViewMatrix { get; } = new Uniform<Matrix4>("view_matrix");

        public Uniform<Matrix4> ModelMatrix { get; } = new Uniform<Matrix4>("model_matrix");

        public Uniform<GLTexture2D> Tex0 { get; } = new Uniform<GLTexture2D>("tex0");

        public Uniform<Vector4> Tint { get; } = new Uniform<Vector4>("tint");

        public Uniform<Vector4> SourceRect { get; } = new Uniform<Vector4>("source_rect");

        public void Render(Mesh mesh, Material material, Transform transform, Camera camera)
        {
            using (Use())
            {
                Setup(mesh, material, transform, camera);

                mesh?.Render();
            }
        }

        protected virtual void Setup(Mesh mesh, Material material, Transform transform, Camera camera)
        {
            if (mesh is not null)
            {
                using (mesh.VertexArray.Bind())
                {
                    Position.Setup(mesh.VertexBuffer, Vertex.PositionOffset);
                    TexCoord.Setup(mesh.VertexBuffer, Vertex.TexCoordOffset);
                    Color.Setup(mesh.VertexBuffer, Vertex.ColorOffset);
                    Normal.Setup(mesh.VertexBuffer, Vertex.NormalOffset);
                }
            }

            if (material is not null)
            {
                Tint.SetValue(material.Tint);
                Tex0.SetValue(material.Texture?.GLTexture);
                SourceRect.SetValue(material.Texture?.SourceRectangle.ToVector() ?? new Vector4(0, 0, 1, 1));
            }

            if (camera is not null)
            {
                ProjectionMatrix.SetValue(camera.ProjectionMatrix);
                ViewMatrix.SetValue(camera.Transform.Matrix);
            }

            ModelMatrix.SetValue(transform.Matrix);
        }
    }
}
