using Vildmark.Graphics.GLObjects;
using OpenTK.Mathematics;
using Vildmark.Graphics.Resources;

namespace Vildmark.Graphics.Models
{
	public class Material
	{
		public Material()
			: this(null, Vector4.One)
		{

		}

		public Material(Vector4 tint)
			: this(null, tint)
		{
		}

		public Material(GLTexture2D texture)
			: this(texture, Vector4.One)
		{
		}

		public Material(GLTexture2D texture, Vector4 tint)
		{
			Texture = texture ?? Textures.WhitePixel;
			Tint = tint;
		}

		public GLTexture2D Texture { get; }

		public Vector4 Tint { get; }
	}
}