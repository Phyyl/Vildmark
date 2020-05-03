using OpenTK.Graphics.OpenGL;
using Vildmark.GLObjects;

namespace Vildmark.Rendering
{
	internal class MaterialShader : Shader
	{
		public MaterialShader(GLShader vertexShader, GLShader fragmentShader)
			: base(vertexShader, fragmentShader)
		{
			ProjectionMatrix = new Matrix4Uniform(this, "projection_matrix");
			Tint = new Vector4Uniform(this, "tint");
			Tex0 = new Sampler2DUniform(this, "tex0", 0);

			Position = new Attrib<Vertex>(this, "vert_position", 3, VertexAttribPointerType.Float, Vertex.Size, Vertex.PositionOffset);
			TexCoord = new Attrib<Vertex>(this, "vert_texcoord", 2, VertexAttribPointerType.Float, Vertex.Size, Vertex.TexCoordOffset);
			Normal = new Attrib<Vertex>(this, "vert_normal", 3, VertexAttribPointerType.Float, Vertex.Size, Vertex.NormalOffset);
		}

		public Attrib<Vertex> Position { get; }

		public Attrib<Vertex> TexCoord { get; }

		public Attrib<Vertex> Normal { get; }

		public Matrix4Uniform ProjectionMatrix { get; }

		public Vector4Uniform Tint { get; }

		public Sampler2DUniform Tex0 { get; }
	}
}
