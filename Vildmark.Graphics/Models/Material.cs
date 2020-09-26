using Vildmark.Graphics.GLObjects;
using OpenTK.Mathematics;
using Vildmark.Graphics.Resources;
using System.Drawing;

namespace Vildmark.Graphics.Models
{
	public class Material
	{
		private static readonly RectangleF defaultSourceRect = new RectangleF(0, 0, 1, 1);

		public Material()
			: this(null, Vector4.One, defaultSourceRect)
		{

		}

		public Material(Vector4 tint)
			: this(null, tint, defaultSourceRect)
		{
		}

		public Material(GLTexture2D texture)
			: this(texture, Vector4.One, defaultSourceRect)
		{
		}

		public Material(GLTexture2D texture, RectangleF sourceRect)
			: this(texture, Vector4.One, sourceRect)
		{
		}

		public Material(GLTexture2D texture, Vector4 tint)
			: this(texture, tint, defaultSourceRect)
		{
		}

		public Material(GLTexture2D texture, Vector4 tint, RectangleF sourceRect)
		{
			Texture = texture ?? Textures.WhitePixel;
			Tint = tint;
		}

		public GLTexture2D Texture { get; }

		public Vector4 Tint { get; }

		public RectangleF SourceRect { get; }
	}
}