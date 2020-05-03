using OpenToolkit;
using OpenToolkit.Graphics.OpenGL;
using OpenToolkit.Mathematics;
using Vildmark.Graphics.Cameras;
using Vildmark.Graphics.GLObjects;

namespace Vildmark.Rendering
{
	internal class Model
	{
		private static GLTexture2D defaultTexture;

		private GLTexture2D DefaultTexture => defaultTexture ?? (defaultTexture = new GLTexture2D(1, 1, new byte[] { 255, 255, 255, 255 }));

		private Vector3 position;

		private Vector3 rotation;

		public Model(Mesh mesh, Material material, PrimitiveType primitiveType = PrimitiveType.Triangles)
		{
			Mesh = mesh;
			Material = material;
			PrimitiveType = primitiveType;

			VertexArray = new GLVertexArray();
			VertexBuffer = new GLBuffer<Vertex3D>(Mesh.Vertices.Span);
			IndexBuffer = Mesh.Indices.IsEmpty ? default : new GLBuffer<uint>(Mesh.Indices.Span, BufferTarget.ElementArrayBuffer);

			Initialize();
		}

		public ref Vector3 Position => ref position;

		public ref Vector3 Rotation => ref rotation;

		public Mesh Mesh { get; }

		public Material Material { get; }

		public GLBuffer<Vertex3D> VertexBuffer { get; }

		public GLBuffer<uint> IndexBuffer { get; }

		public GLVertexArray VertexArray { get; }

		public PrimitiveType PrimitiveType { get; }

		public Matrix4 ModelMatrix => Matrix4.CreateTranslation(-position) * Matrix4.CreateRotationY(rotation.Y) * Matrix4.CreateRotationX(rotation.X) * Matrix4.CreateRotationX(rotation.Z);

		public void Draw(Camera camera)
		{
			Matrix4 projectionMatrix = ModelMatrix * camera.ViewMatrix * camera.ProjectionMatrix;
			GLTexture2D texture = Material.Texture ?? DefaultTexture;

			using (Material.Shader.ShaderProgram.Use())
			{
				Material.Shader.Tex0.SetValue(texture);
				Material.Shader.ProjectionMatrix.SetValue(projectionMatrix);
				Material.Shader.Tint.SetValue(Material.Color);

				using (VertexArray.Bind())
				{
					if (IndexBuffer != default)
					{
						GL.DrawElements(PrimitiveType, IndexBuffer.Count, DrawElementsType.UnsignedInt, 0);
					}
					else
					{
						GL.DrawArrays(PrimitiveType, 0, VertexBuffer.Count);
					}
				}
			}
		}

		private void Initialize()
		{
			using (VertexArray.Bind())
			{
				IndexBuffer?.Bind();

				using (VertexBuffer.Bind())
				{
					VertexBuffer.VertexAttribPointer(Material.Shader.Position.Location, 3, VertexAttribPointerType.Float, Vertex3D.Size, Vertex3D.PositionOffset);
					VertexBuffer.VertexAttribPointer(Material.Shader.TexCoord.Location, 2, VertexAttribPointerType.Float, Vertex3D.Size, Vertex3D.TexCoordOffset);
				}
			}
		}
	}
}
