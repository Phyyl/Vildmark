using OpenTK.Mathematics;
using Vildmark.Graphics.GLObjects;
using Vildmark.Graphics.Models;
using Vildmark.Graphics.Shaders;

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
	}
}
