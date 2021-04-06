using OpenTK.Graphics.OpenGL;
using System;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Meshes
{
    public class Mesh<TVertex> : IMesh<TVertex> where TVertex : unmanaged
    {
        public GLVertexArray VertexArray { get; } = new GLVertexArray();

        public GLBuffer<TVertex> VertexBuffer { get; }

        public Mesh(Span<TVertex> vertices = default)
        {
            VertexBuffer = new GLBuffer<TVertex>(vertices);
        }

        public virtual void Render(PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            GL.DrawArrays(primitiveType, 0, VertexBuffer.Count);
        }

        public virtual void SetupShader(IShader shader)
        {
            VertexArray.Bind();
        }
    }

    public class Mesh : Mesh<Vertex>
    {
        public Mesh(Span<Vertex> vertices = default)
            : base(vertices)
        {
        }

        public override void SetupShader(IShader shader)
        {
            base.SetupShader(shader);

            if (shader is IColorShader colorShader)
            {
                colorShader.Color.Setup(VertexBuffer, Vertex.ColorOffset);
            }

            if (shader is IPositionShader positionShader)
            {
                positionShader.Position.Setup(VertexBuffer, Vertex.PositionOffset);
            }

            if (shader is INormalShader normalShader)
            {
                normalShader.Normal.Setup(VertexBuffer, Vertex.NormalOffset);
            }

            if (shader is ITexCoordShader texCoordShader)
            {
                texCoordShader.TexCoord.Setup(VertexBuffer, Vertex.TexCoordOffset);
            }
        }
    }
}

