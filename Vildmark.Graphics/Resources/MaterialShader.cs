using OpenToolkit.Mathematics;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Shaders;

namespace Vildmark.Graphics.Resources
{
	public class MaterialShader : EmbeddedShader
	{
		public MaterialShader()
			: this("material")
		{
		}

		protected MaterialShader(string name)
			: base(name)
		{
			ProjectionMatrix = new Uniform<Matrix4>(this, "projection_matrix");
			ModelMatrix = new Uniform<Matrix4>(this, "model_matrix");
			ViewMatrix = new Uniform<Matrix4>(this, "view_matrix");
			Tex0 = new Uniform<GLTexture2D>(this, "tex0");
			Tint = new Uniform<Vector4>(this, "tint");

			Position = new Attrib<Vertex>(this, "vert_position", 3);
			TexCoord = new Attrib<Vertex>(this, "vert_tex_coord", 2);
			Color = new Attrib<Vertex>(this, "vert_color", 4);
			Normal = new Attrib<Vertex>(this, "vert_normal", 3);
		}

		public Attrib<Vertex> Position { get; }

		public Attrib<Vertex> TexCoord { get; }

		public Attrib<Vertex> Color { get; }

		public Attrib<Vertex> Normal { get; }

		public Uniform<Matrix4> ProjectionMatrix { get; }

		public Uniform<Matrix4> ViewMatrix { get; }

		public Uniform<Matrix4> ModelMatrix { get; }

		public Uniform<GLTexture2D> Tex0 { get; }

		public Uniform<Vector4> Tint { get; }
	}
}
