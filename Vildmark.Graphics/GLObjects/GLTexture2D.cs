using OpenToolkit.Graphics.OpenGL;
using OpenToolkit.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace Vildmark.Graphics.GLObjects
{
	public class GLTexture2D : GLObject
	{
		private const PixelInternalFormat pixelInternalFormat = PixelInternalFormat.Rgba;
		private const PixelFormat pixelFormat = PixelFormat.Bgra;
		private const PixelType pixelType = PixelType.UnsignedByte;

		public GLTexture2D(
			int width,
			int height,
			Span<byte> data = default,
			TextureMagFilter textureMagFilter = TextureMagFilter.Nearest,
			TextureMinFilter textureMinFilter = TextureMinFilter.Linear,
			TextureWrapMode wrapSMode = TextureWrapMode.ClampToEdge,
			TextureWrapMode wrapTMode = TextureWrapMode.ClampToEdge,
			TextureTarget textureTarget = TextureTarget.Texture2D)
			: base(GL.GenTexture())
		{
			TextureTarget = textureTarget;
			TextureMagFilter = textureMagFilter;
			TextureMinFilter = textureMinFilter;
			WrapSMode = wrapSMode;
			WrapTMode = wrapTMode;

			SetData(width, height, data);
		}

		public TextureTarget TextureTarget { get; }

		public TextureMagFilter TextureMagFilter { get; }

		public TextureMinFilter TextureMinFilter { get; }

		public TextureWrapMode WrapSMode { get; }

		public TextureWrapMode WrapTMode { get; }

		public int Width { get; private set; }

		public int Height { get; private set; }

		public Vector2 Size => new Vector2(Width, Height);

		public float TexelWidth => 1 / (float)Width;

		public float TexelHeight => 1 / (float)Height;

		public Vector2 TexelSize => new Vector2(TexelWidth, TexelHeight);

		public void UpdateData<T>(int x, int y, int width, int height, Span<T> data) where T : unmanaged
		{
			if (data.IsEmpty)
			{
				return;
			}

			GL.TexSubImage2D(TextureTarget, 0, x, y, width, height, pixelFormat, pixelType, ref MemoryMarshal.GetReference(data));
		}

		public void SetData<T>(int width, int height, Span<T> data) where T : unmanaged
		{
			using (Bind())
			{
				if (data.IsEmpty)
				{
					GL.TexImage2D(TextureTarget, 0, pixelInternalFormat, width, height, 0, pixelFormat, pixelType, IntPtr.Zero);
				}
				else
				{
					GL.TexImage2D(TextureTarget, 0, pixelInternalFormat, width, height, 0, pixelFormat, pixelType, ref MemoryMarshal.GetReference(data));
				}

				Width = width;
				Height = height;

				GL.TexParameter(TextureTarget, TextureParameterName.TextureMagFilter, (int)TextureMagFilter);
				GL.TexParameter(TextureTarget, TextureParameterName.TextureMinFilter, (int)TextureMinFilter);

				GL.TexParameter(TextureTarget, TextureParameterName.TextureWrapS, (int)WrapSMode);
				GL.TexParameter(TextureTarget, TextureParameterName.TextureWrapT, (int)WrapTMode);
			}
		}

		public void Resize(int width, int height)
		{
			SetData<byte>(width, height, default);
		}

		public IDisposable Bind(int index = 0)
		{
			return new BindContext(this, index);
		}

		public void Unbind(int index = 0)
		{
			GL.ActiveTexture(TextureUnit.Texture0 + index);
			GL.BindTexture(TextureTarget, 0);
		}

		public GLSLSampler2D GetSampler(int index = 0)
		{
			return new GLSLSampler2D(this, index);
		}

		protected override void DisposeOpenGL()
		{
			GL.DeleteTexture(this);
		}

		private class BindContext : IDisposable
		{
			public GLTexture2D Texture { get; }

			public int Index { get; }

			public BindContext(GLTexture2D texture, int index, bool bind = true)
			{
				Texture = texture;
				Index = index;

				if (bind)
				{
					GL.ActiveTexture(TextureUnit.Texture0 + Index);
					GL.BindTexture(texture.TextureTarget, Texture);
				}
			}

			public void Dispose()
			{
				Texture.Unbind(Index);
			}
		}
	}
}
